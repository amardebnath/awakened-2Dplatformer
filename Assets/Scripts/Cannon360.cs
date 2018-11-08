using UnityEngine;
using System.Collections;

public class Cannon360 : MonoBehaviour {

	// Use this for initialization
	//public float radius;
	public GameObject Gola;
	public float adjustAngle;
	public float firingTime;

	private float time = 0;
	private GameObject player;
	private float angle;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}

	// Update is called once per frame
	void Update () {
		angle = Mathf.Atan2 (player.transform.position.y - transform.position.y, 
			player.transform.position.x - transform.position.x) * (180f / Mathf.PI);

		Quaternion rot = Quaternion.AngleAxis (angle + adjustAngle, Vector3.forward);
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
