using UnityEngine;
using System.Collections;

public class bombExplosion : MonoBehaviour {

	public float waitingTime;
	public float smooth;
	public GameObject explosion;

	private SpriteRenderer sr;
	private float timeElapsed = 0;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		sr.color = Color.Lerp(sr.color, new Color(1f, 1f, 1f, 0f), Time.deltaTime * smooth);
		timeElapsed += Time.deltaTime;
		if (timeElapsed > waitingTime) {
			Destroy (explosion);
		}
	}
	void getDirection(float dir){
		if (dir < 0) {
			transform.localScale = new Vector3 (transform.localScale.x * -1f, transform.localScale.y, 1f);
		}
	}
}