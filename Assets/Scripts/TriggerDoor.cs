using UnityEngine;
using System.Collections;

public class TriggerDoor : MonoBehaviour {

	public GameObject openedDoor;
	public int color;

	private bool doorOpened = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			if(color == 1){
				if(other.GetComponent<Player_Controller>().isBlue){
					doorOpened = true;
				}
			} else if(color == 2){
				if(other.GetComponent<Player_Controller>().isRed){
					doorOpened = true;
				}
			} else {
				if(other.GetComponent<Player_Controller>().isYellow){
					doorOpened = true;
				}
			}
			if (doorOpened) {
				Transform doorPosition = transform.parent;
				GameObject temp = (GameObject)Instantiate (openedDoor, doorPosition.position, Quaternion.identity);
				Destroy (doorPosition.gameObject);
			}
		}
	}
}