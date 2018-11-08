using UnityEngine;
using System.Collections;

public class sphere : MonoBehaviour {

	public GameObject thisGameobject;
	public float managePositionX;
	public float managePositionY;
	public float f;
	public float timeOut;

	private float forceX;
	private float forceY;
	private float direction;
	private float timeElapsed;
	private Collider2D col;
	private Rigidbody2D rb;
	private Vector3 position;
	private Vector3 playerPosition;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		if (direction == 1) {
			position.x = transform.position.x;// + managePositionX;
		} else {
			position.x = transform.position.x;// - managePositionX;
		}
		position.y = transform.position.y + managePositionY;
		transform.position = position;
		if (position.x < playerPosition.x) {
			forceX = f * Mathf.Abs((position.x-playerPosition.x)/(Mathf.Abs(position.x-playerPosition.x) + Mathf.Abs(position.y-playerPosition.y)));
		} else {
			forceX = -f * Mathf.Abs((position.x-playerPosition.x)/(Mathf.Abs(position.x-playerPosition.x) + Mathf.Abs(position.y-playerPosition.y)));
		}
		if (position.y < playerPosition.y) {
			forceY = f * Mathf.Abs((position.y-playerPosition.y)/(Mathf.Abs(position.x-playerPosition.x) + Mathf.Abs(position.y-playerPosition.y)));
		} else {
			forceY = -f * Mathf.Abs((position.y-playerPosition.y)/(Mathf.Abs(position.x-playerPosition.x) + Mathf.Abs(position.y-playerPosition.y)));
		}
		timeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddForce (new Vector2(forceX, forceY), ForceMode2D.Impulse);
		timeElapsed += Time.deltaTime;
		if (timeElapsed > timeOut) {
			Destroy(thisGameobject);
		}
			
	}
	void getPlayerTransform(Vector3 pos){
		playerPosition = pos;
	}
	void getFearScale(float dir){
		direction = dir;
	}
	void OnCollisionEnter2D(Collision2D other){
		Destroy(thisGameobject);
	}
}