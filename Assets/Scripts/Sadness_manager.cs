using UnityEngine;
using System.Collections;

public class Sadness_manager : MonoBehaviour {


	private Animator sadAnim;
	public GameObject sadness;
	public GameObject player;
	public GameObject dead;
	public GameObject bubble;
	public int health;
	public float radius;
	public float time;
	public bool byPrincess;

	private float elapsedTime;
	private float bombDir;
	private Rigidbody2D sadRb;
	// Use this for initialization
	void Start () {
		sadAnim = GetComponent<Animator> ();
		sadRb = GetComponent<Rigidbody2D> ();
		elapsedTime = 0;
		player = GameObject.Find ("Player");
		if (GameController.gameController.iteration != 0)
			health = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.x > transform.position.x) {
		transform.localScale = new Vector3 (-Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);;
		} else {
			transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}

		if (sadAnim.GetCurrentAnimatorStateInfo (0).IsName ("Sadness_hurt_backward")) {
			sadAnim.SetBool ("isHurt", false);
			//transform.localScale = new Vector3 (1f, 1f, 1f);
		}
		Vector3 posSad = transform.position;
		Vector3 posPlayer = player.transform.position;
		float playerDistance = Mathf.Sqrt ((posSad.x - posPlayer.x) * (posSad.x - posPlayer.x) + (posSad.y - posPlayer.y) * (posSad.y - posPlayer.y));
		if (playerDistance < radius) {
			elapsedTime += Time.deltaTime;
			if (elapsedTime > time) {
				GameObject tempBubble = (GameObject)Instantiate (bubble, player.transform.position, Quaternion.identity);
				tempBubble.GetComponent<drainLife> ().sadness = sadness;
				tempBubble.GetComponent<drainLife> ().player = player;
				elapsedTime = 0;
			}
			//player.SendMessage ("sadAttack");  //draining life of player
		} else {
			elapsedTime = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Bomb")) {
			health--;
			if (health == 0) {
					GameObject temp = (GameObject)Instantiate (dead, transform.position, Quaternion.identity);
					temp.SendMessage ("getBombDir", other.transform.localScale.x);
			
					if (byPrincess) {
						GameObject princess = GameObject.Find ("Princess");
						princess.GetComponent<PrincessFinal> ().enemyDied ();
					}
					Destroy (sadness);

			}
			if (other.transform.localScale.x > 0f) {
				bombDir = 1f;
			} else {
				bombDir = -1f;
			}
			transform.localScale = new Vector3(bombDir * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1f);
			sadAnim.SetBool ("isHurt", true);
		}
	}
		
}
