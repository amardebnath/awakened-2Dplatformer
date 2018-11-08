using UnityEngine;
using System.Collections;

public class BladeWheelMovement : MonoBehaviour {

	public float range;
	public float velocityX;
	public bool isVertical;

	private Rigidbody2D rb;
	private float startingY;
	private float startingX;
	private Vector2 velocity;
	// Use this for initialization
	void Start () {
		startingX = transform.position.x;
		startingY = transform.position.y;
		rb = GetComponent<Rigidbody2D> ();
		if (isVertical) {
			velocity = new Vector2 (0f, velocityX);
		} else {
			velocity = new Vector2 (velocityX, 0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isVertical) {
			if (transform.position.y > startingY + range) {
				velocity = new Vector2 (0f, -velocityX);
			} else if (transform.position.y < startingY - range) {
				velocity = new Vector2 (0f, velocityX);
			}
		} else {
			if (transform.position.x > startingX + range) {
				velocity = new Vector2 (-velocityX, 0f);
			} else if (transform.position.x < startingX - range) {
				velocity = new Vector2 (velocityX, 0f);
			}
		}
		rb.velocity = velocity;
	}
}
