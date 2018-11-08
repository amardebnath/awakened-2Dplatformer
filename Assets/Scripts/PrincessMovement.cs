using UnityEngine;
using System.Collections;

public class PrincessMovement : MonoBehaviour {

	public Vector3[] positions;
	public float Radius;
	public Vector2 velocity;
	public float waitingTime;
	public float smooth;
	public bool[] movingLeft;

	private Color defaultColor;
	private SpriteRenderer sr;
	private float time;
	private Rigidbody2D rb;
	private GameObject player;
	public int i = 0;
	private bool isInRadius;
	// Use this for initialization
	void Start () {
		if (GameController.gameController.Continue == true)
			i = GameController.gameController.PI;
		GetComponent<PrincessFinal> ().enabled = false;
		transform.position = positions [i];
		player = GameObject.Find ("Player");
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		time = 0;
		defaultColor = sr.color;
		print ("resumed");
	}
	
	// Update is called once per frame
	void Update () {
		//print (i + " " + GameController.gameController.PI);
		if (!isInRadius) {
			float distanceX = Mathf.Abs (player.transform.position.x - transform.position.x);
			float distanceY = Mathf.Abs (player.transform.position.y - transform.position.y);
			if (distanceX < Radius && distanceY < Radius) {
				isInRadius = true;
			}
		} else {
			if (movingLeft[i]) {
				transform.localScale = new Vector3 (-Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
				rb.velocity = new Vector2 (-velocity.x, velocity.y);
			} else {
				rb.velocity = velocity;
				transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}
			sr.color = Color.Lerp (sr.color, new Color (1f, 1f, 1f, 0f), smooth * Time.deltaTime);
			time += Time.deltaTime;
		}
		if (time > waitingTime) {
			isInRadius = false;
			time = 0;
			i++;
			sr.color = defaultColor;
			if (i < positions.Length) {
				transform.position = positions [i];
				isInRadius = false;
				rb.velocity = new Vector2 (0f, 0f);
			} else {
				GetComponent<PrincessFinal> ().enabled = true;
				GetComponent<PrincessMovement> ().enabled = false;
			}
		}
	}
}