using UnityEngine;
using System.Collections;

public class cannonRotation : MonoBehaviour {

	//public float radius;
	public float adjustAngleRight;
	public float adjustAngleLeft;
	public float firingTime;
	public GameObject Gola;

	private GameObject player;
	private float angle;
	private float time = 0;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x > 0) {
			angle = Mathf.Atan2 (player.transform.position.y - transform.position.y, 
				player.transform.position.x - transform.position.x) * (180f/Mathf.PI);
			
			Quaternion rot = Quaternion.AngleAxis(angle + adjustAngleRight, Vector3.forward);
			if (rot.eulerAngles.z > 300 || rot.eulerAngles.z < 25) {
				transform.rotation = rot;
				time += Time.deltaTime;
				if (time > firingTime) {
					time = 0;
					Transform head = transform.GetChild (0);
					GameObject tempSphere = (GameObject)Instantiate (Gola, head.transform.position, Quaternion.identity);
					tempSphere.SendMessage ("getPlayerTransform", player.transform.position);
					tempSphere.SendMessage ("getFearScale", transform.localScale.x);
				}
			}
		} else {
			angle = Mathf.Atan2 (player.transform.position.y - transform.position.y, 
				player.transform.position.x - transform.position.x) * (180f/Mathf.PI);

			Quaternion rot = Quaternion.AngleAxis(angle + adjustAngleLeft, Vector3.forward);
			if (rot.eulerAngles.z > 315 || rot.eulerAngles.z < 60) {
				transform.rotation = rot;
				time += Time.deltaTime;
				if (time > firingTime) {
					time = 0;
					Transform head = transform.GetChild (0);
					GameObject tempSphere = (GameObject)Instantiate (Gola, head.transform.position, Quaternion.identity);
					tempSphere.SendMessage ("getPlayerTransform", player.transform.position);
					tempSphere.SendMessage ("getFearScale", transform.localScale.x);
				}
			}
		}
	}
}
