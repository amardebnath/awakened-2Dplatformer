using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

	public Transform[] backGrounds;
	private float[] parallaxScales;
	public float smooth;

	private Transform cam;
	private Vector3 prevCamPos;


	void Awake(){
		cam = Camera.main.transform;
	}
	// Use this for initialization
	void Start () {
		prevCamPos = cam.position;

		parallaxScales = new float[backGrounds.Length];
		for (int i = 0; i < backGrounds.Length; i++) {
			parallaxScales [i] = backGrounds [i].position.z * (-1f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < backGrounds.Length; i++) {
			float parallax = (prevCamPos.x - cam.position.x) * parallaxScales [i];
			float backTargetPosX = backGrounds [i].position.x + parallax;
			Vector3 backTargetPos = new Vector3 (backTargetPosX, backGrounds [i].position.y, backGrounds [i].position.z);
			backGrounds [i].position = Vector3.Lerp (backGrounds [i].position, backTargetPos, smooth * Time.deltaTime);
		}
		prevCamPos = cam.position;
	}
}
