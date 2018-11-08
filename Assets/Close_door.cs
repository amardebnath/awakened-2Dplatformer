using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_door : MonoBehaviour {

	public bool isRight;
	public GameObject closed_pass;

	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		if (isRight) {
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, 1f);
		}
	}

	// Update is called once per frame
	void Update () {
		if (isRight) {
			if (player.transform.position.x + 1.0f < transform.position.x) {
				GameObject temp = (GameObject)Instantiate (closed_pass, transform.position, Quaternion.identity);
				temp.GetComponent<Open_pass> ().isRight = isRight;
				Destroy (this.gameObject);
			}
		} else {
			if (player.transform.position.x > transform.position.x + 1.0f) {
				GameObject temp = (GameObject)Instantiate (closed_pass, transform.position, Quaternion.identity);
				temp.GetComponent<Open_pass> ().isRight = isRight;
				Destroy (this.gameObject);
			}
		}
	}
}
