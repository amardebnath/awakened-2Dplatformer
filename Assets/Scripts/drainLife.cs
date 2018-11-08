using UnityEngine;
using System.Collections;

public class drainLife : MonoBehaviour {

	public GameObject thisBubble;
	public GameObject player;
	public GameObject sadness;
	public float speed;
	public bool active;
	// Use this for initialization
	private float timeElapsed;
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (sadness != null) {
			transform.position = Vector3.Lerp (transform.position, sadness.transform.position, Time.deltaTime * speed);
		} else {
			Destroy (sadness);
		}
		timeElapsed += Time.deltaTime;
		if (timeElapsed > 5.0f) {
			Destroy (thisBubble);
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Sadness")) {
			Destroy (thisBubble);
			player.GetComponent<Player_Damage> ().damageCount = player.GetComponent<Player_Damage> ().damageCount- 0.5f;
		}
	}
}