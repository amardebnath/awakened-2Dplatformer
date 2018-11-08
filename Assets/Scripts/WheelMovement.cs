using UnityEngine;
using System.Collections;

public class WheelMovement : MonoBehaviour {

	public Vector3[] positions;
	public float VelocityX;
	public float VelocityY;
	public float destroyTime;
	public float smooth;

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Vector3 target;
	private Vector2 velocity;
	public bool isHorizontal;
	private bool isDestroyed;
	private float time;
	public int i = 0;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		transform.position = positions [i];
		target = positions [i + 1];
		if (Mathf.Abs (target.y - positions [i].y) < 0.5f) {
			isHorizontal = true;
		}
		print (positions[i]);
		print (target);
		print (isHorizontal);
		if (isHorizontal) {
			if (positions[i].x < target.x) {
				velocity = new Vector2 (VelocityX, 0f);
				print("Forward");
			} else {
				velocity = new Vector2 (-VelocityX, 0f);
			}
		} else {
			if (positions[i].y < target.y) {
				velocity = new Vector2 (0f, VelocityY);
			} else {
				velocity = new Vector2 (0f, -VelocityY);
			}
		}
		print (positions [i]);
		print (target);
	}
	
	// Update is called once per frame
	void Update () {
		if (isDestroyed) {
			sr.color = Color.Lerp (sr.color, new Color (1f, 1f, 1f, 0f), smooth * Time.deltaTime);
			time += Time.deltaTime;
			if (time > destroyTime) {
				velocity = new Vector2 (0f, 0f);
				Destroy (gameObject);
			}
		} else {
			if (isHorizontal) {
				if (positions [i].x < target.x) {
					if (transform.position.x > target.x) {
						i++;
						ChangeDirection ();
					}
				} else {
					if (transform.position.x < target.x) {
						i++;
						ChangeDirection ();
					}
				}
			} else {
				if (positions [i].y < target.y) {
					if (transform.position.y > target.y) {
						i++;
						ChangeDirection ();
					}
				} else {
					if (transform.position.y < target.y) {
						i++;
						ChangeDirection ();
					}
				}
			}
		}
		rb.velocity = velocity;
		print (velocity);
	}

	void ChangeDirection(){
		if (!(i < positions.Length - 1)) {
			isDestroyed = true;
			target = transform.position;
			velocity = new Vector2 (0f, 0f);
			rb.velocity = new Vector2 (0f, 0f);
		} else {
			target = positions [i + 1];
			if (Mathf.Abs (target.y - positions [i].y) < 0.5f) {
				isHorizontal = true;
			} else {
				isHorizontal = false;
			}
			if (isHorizontal) {
				if (positions [i].x < target.x) {
					velocity = new Vector2 (VelocityX, 0f);
				} else {
					velocity = new Vector2 (-VelocityX, 0f);
				}
			} else {
				if (positions [i].y < target.y) {
					velocity = new Vector2 (0f, VelocityY);
				} else {
					velocity = new Vector2 (0f, -VelocityY);
				}
			}
		}
		print (positions [i]);
		print (target);
	}
}