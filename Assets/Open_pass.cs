using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_pass : MonoBehaviour {
	public bool isRight;
	public GameObject opened_pass;

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
			if (player.transform.position.x > transform.position.x) {
				GameObject temp = (GameObject)Instantiate (opened_pass, transform.position, Quaternion.identity);
				temp.GetComponent<Close_door> ().isRight = isRight;
				Destroy (this.gameObject);
			}
		} else {
			if (player.transform.position.x < transform.position.x) {
				GameObject temp = (GameObject)Instantiate (opened_pass, transform.position, Quaternion.identity);
				temp.GetComponent<Close_door> ().isRight = isRight;
				Destroy (this.gameObject);
			}
		}
	}
}
