using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	public float smooth;
	public float f;
	public GameObject dead_body;
	private Rigidbody2D[] part_rb;
	public GameObject[] parts;
	public int size;

	private float bombDir;

	private float timer;
	private SpriteRenderer[] spriteRend;
	// Use this for initialization
	void Start () {
		spriteRend = GetComponentsInChildren<SpriteRenderer> ();
		part_rb = GetComponentsInChildren<Rigidbody2D> ();
		timer = 0;
		for(int i = 0; i < size; i++){
			part_rb [i].AddForce (new Vector2(parts[i].transform.position.x - transform.position.x, parts[i].transform.position.y - 
				transform.position.y) * f, ForceMode2D.Impulse);
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		for (int i = 0; i < size; i++) {

			if (timer > 1.5) {
				spriteRend [i].color = Color.Lerp (spriteRend [i].color, new Color (1f, 1f, 1f, 0f), Time.deltaTime * smooth);
			}
		}
		if (timer > 3) {
			Destroy (dead_body);
		}
	}

	void FixedUpdate(){
		
	}
		
	public void getBombDir(float dir){
		bombDir = dir;
	}
}