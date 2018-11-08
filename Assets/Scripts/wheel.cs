using UnityEngine;
using System.Collections;

public class wheel : MonoBehaviour {

	public float rotate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0f, 0f, rotate);
	}
}
