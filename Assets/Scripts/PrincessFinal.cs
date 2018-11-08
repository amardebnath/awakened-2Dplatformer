using UnityEngine;
using System.Collections;

public class PrincessFinal : MonoBehaviour {

	public float groundTime;
	public float flyingTime;
	public Vector3 offset;
	public float offsetGround;
	public GameObject player;
	public GameObject anger;
	public GameObject fear;
	public GameObject sadness;
	public GameObject key;
	public GameObject Dead;
	public int maxEnemyOnScreen;
	public int remainingEnemy;
	public float velocityX, velocityY;
	public float maxHeight;
	public float enemyTime;
	public bool isGrounded;
	public Vector3[] positions;
	public float radius;

	private float timeE;
	private int enemyCount;
	private Rigidbody2D rb;
	private Animator anim;
	public float elapsedTime = 0;
	private bool isFlying;
	private bool isGoingUp;
	private float startingHeight;
	private int i = 0;

	// Use this for initialization
	void Start () {
		GetComponent<PrincessMovement> ().enabled = false;
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = new Vector2 (0f, 0f);
		anim = GetComponent<Animator> ();
		player = GameObject.Find ("Player");
		timeE = 1.5f;
		transform.position = positions [0];
	}
	
	// Update is called once per frame
	void Update () {
		if (remainingEnemy == 0 && enemyCount == 0) {
			Destroy (gameObject);

			GameObject tempKey = (GameObject)Instantiate (key, transform.position, Quaternion.identity);
			GameObject temp = (GameObject)Instantiate (Dead, transform.position, Quaternion.identity);
			if (player.transform.localScale.x > 0) {
				temp.GetComponent<Death> ().getBombDir (1f);
			} else {
				temp.GetComponent<Death> ().getBombDir (-1f);
			}
		}
		if (enemyCount < maxEnemyOnScreen && remainingEnemy > 0) {
			if (timeE > enemyTime) {
				int c = Random.Range (0, 5);
				print (c);
				if (c == 1) {
					//instantiate fear
					GameObject temp = (GameObject)Instantiate (fear, (transform.position + player.transform.position) / 2f,
						Quaternion.identity);
					temp.SetActive (true);
					temp.GetComponent<Fear_Manager> ().byPrincess = true;
					enemyCount++;
					remainingEnemy--;
					timeE = 0;
					anim.SetBool ("isSummoning", true);
				} else if (c == 2) {
					//instantiate anger
					GameObject temp;
					if (isGrounded) {
						temp = (GameObject)Instantiate (anger, new Vector3 ((transform.position.x + player.transform.position.x) / 2f, 
							transform.position.y), Quaternion.identity);
					} else {
						temp = (GameObject)Instantiate (anger, new Vector3 ((transform.position.x + player.transform.position.x) / 2f,
							startingHeight), Quaternion.identity);
					}
					temp.SetActive (true);
					temp.GetComponent<Anger> ().byPrincess = true;
					enemyCount++;
					remainingEnemy--;
					timeE = 0;
					anim.SetBool ("isSummoning", true);
				} else if (c == 3) {
					//instantiate sadness
					GameObject temp;
					if (isGrounded) {
						temp = (GameObject)Instantiate (sadness, new Vector3 ((transform.position.x + player.transform.position.x) / 2f,
							transform.position.y - 0.5f), Quaternion.identity);
					} else {
						temp = (GameObject)Instantiate (sadness, new Vector3 ((transform.position.x + player.transform.position.x) / 2f,
							startingHeight - 0.5f), Quaternion.identity);
					}
					temp.SetActive (true);
					temp.GetComponent<Sadness_manager> ().byPrincess = true;
					enemyCount++;
					remainingEnemy--;
					timeE = 0;
					anim.SetBool ("isSummoning", true);
				} else if (c == 4) {
					//instantiate shame
				}
			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsTag("Summon")){
				anim.SetBool ("isSummoning", false);
			}
		}

		if (!isFlying) {
			if (!isGrounded) {
				rb.velocity = new Vector2 (0f, -velocityY);
			}
		} else if (isGoingUp) {
			rb.velocity = new Vector2 (0f, velocityY);
			if (transform.position.y > startingHeight + maxHeight) {
				rb.velocity = new Vector2(0f, 0f);
				isGoingUp = false;
			}
		}

		elapsedTime += Time.deltaTime;
		if (isFlying) {
			if (elapsedTime > flyingTime) {
				isFlying = false;
				elapsedTime = 0;
			}
		} else {
			if (elapsedTime > groundTime) {
				isFlying = true;
				isGoingUp = true;
				isGrounded = false;
				elapsedTime = 0;
				startingHeight = transform.position.y;
				anim.SetBool ("isFlying", true);
			}
		}
		float distance = Mathf.Abs (transform.position.x - player.transform.position.x);
		if (distance < radius) {
			i++;
			if (i == positions.Length) {
				i = 0;
			}
			transform.position = positions [i];
		}
		timeE += Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer == 8) {
			isGrounded = true;
			isFlying = false;
			rb.velocity = new Vector2 (0f, 0f);
			anim.SetBool ("isFlying", false);
			elapsedTime = 0;
		}
	}

	public void enemyDied(){
		enemyCount--;
		if (remainingEnemy == 0 && enemyCount == 0) {
			Destroy (gameObject);
			GameObject tempKey = (GameObject)Instantiate (key, transform.position, Quaternion.identity);
			GameObject temp = (GameObject)Instantiate (Dead, transform.position, Quaternion.identity);
			if (player.transform.localScale.x > 0) {
				temp.GetComponent<Death> ().getBombDir (1f);
			} else {
				temp.GetComponent<Death> ().getBombDir (-1f);
			}
		}
	}
}