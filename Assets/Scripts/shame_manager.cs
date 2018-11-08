using UnityEngine;
using System.Collections;

public class shame_manager : MonoBehaviour {

	private GameObject player;
	public GameObject dead;
	public bool engage;
	public bool isFloating;
	public float zigzagDistance;
	public float observeTime;
	public int health=2;
	public float manageY;

	private bool inRange;
	public bool hasAttacked;
	private float time;
	private Animator animator;
	public float vC;
	private Rigidbody2D rb;
	public bool isAttacking;
	public bool isFirst;
	private float targetY;
	public bool movingRight;
	public bool goingUp;
	public Vector2 v;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		player = GameObject.Find ("Player");
		movingRight = true;
		isFirst = true;
		isAttacking = false;
		goingUp = true;
		time = 0;
		inRange = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (goingUp) {
			Vector3 pos1 = transform.position;
			Vector3 pos2 = player.transform.position;
			float d = Mathf.Abs (pos1.x - pos2.x);
			if (d < 15) {
				inRange = true;
			} else {
				inRange = false;
			}
		}
		if (inRange) {
			if (engage) {
				if (isAttacking) {
					if (!hasAttacked) {
						rb.velocity = v * 2.5f;
						//isAttacking = false;
						rb.isKinematic = false;
						hasAttacked = true;
					}
				} else if (!isFloating) {
					if (transform.position.x < player.transform.position.x) {
						transform.localScale = new Vector3 (-Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
					} else {
						transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
					}
					if (goingUp) {
						if (isFirst) {
							targetY = transform.position.y + zigzagDistance;
							isFirst = false;
						}
						if (movingRight) {
							transform.position += new Vector3 (0.1f, 0.05f);
						} else {
							transform.position += new Vector3 (-0.1f, 0.05f);
						}
						if (transform.position.y >= targetY) {
							targetY += zigzagDistance;
							movingRight = !movingRight;
						}
						Vector2 startPoint = new Vector2 (transform.position.x, transform.position.y - manageY);
						Vector2 midPoint = new Vector2 (transform.position.x, transform.position.y - manageY);
						RaycastHit2D raycast1 = Physics2D.Linecast (startPoint, midPoint);
						if (raycast1.collider == null || raycast1.collider.CompareTag ("Player") || raycast1.collider.CompareTag("Bomb")) {
							isFirst = true;
							isFloating = true;
							time = 0;
							hasAttacked = false;
						}
					} else {
						float targetDepth = 0f;
						if (isFirst) {
							rb.velocity = new Vector2 (0f, 0f);
							targetY = transform.position.y - zigzagDistance;
							isFirst = false;
							targetDepth = transform.position.y - 1.8f;
						}
						if (movingRight) {
							transform.position -= new Vector3 (0.1f, 0.05f);
						} else {
							transform.position -= new Vector3 (-0.1f, 0.05f);
						}
						if (transform.position.y <= targetY) {
							targetY -= zigzagDistance;
							movingRight = !movingRight;
						}
						if (targetDepth > transform.position.y) {
							goingUp = true;
							engage = false;
						}
					}
				} else {
					if (transform.position.x < player.transform.position.x) {
						transform.localScale = new Vector3 (-Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
					} else {
						transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
					}
					time += Time.deltaTime;
					if (time > observeTime) {
						Vector2 startPoint = new Vector2 (player.transform.position.x + transform.localScale.x * -8f, transform.position.y - (manageY + 0.1f));
						Vector2 midPoint = new Vector2 (player.transform.position.x + transform.localScale.x * -8f - 0.5f, transform.position.y);
						Vector2 endPoint = new Vector2 (player.transform.position.x + transform.localScale.x * -8f + 0.5f, transform.position.y - (manageY + 0.1f));
						RaycastHit2D raycast1 = Physics2D.Linecast (startPoint, midPoint);
						RaycastHit2D raycast2 = Physics2D.Linecast (endPoint, midPoint);

						if (raycast1.collider != null && raycast2.collider != null) {
							if (raycast1.collider.gameObject.layer == 8 && raycast2.collider.gameObject.layer == 8 && Mathf.Abs(transform.position.y - player.transform.position.y) < 4.0f) {
								if (Mathf.Abs (player.transform.position.x - transform.position.x) < 5.0f) {
									isAttacking = true;
									vC = Mathf.Sqrt (5f * 4f * Mathf.Abs (player.transform.position.x - transform.position.x));
									if (player.transform.position.x > transform.position.x) {
										v = new Vector2 (vC * 0.707f, vC * 0.707f);
									} else {
										v = new Vector2 (-(vC * 0.707f), vC * 0.707f);
									}
									animator.SetBool ("isAttacking", true);
									goingUp = false;
								}
							}
						}
					}
				}
			} else {
				time += Time.deltaTime;
				if (time > 1f) {
					int rand = (int)Random.Range (1f, 4f);
					if (rand % 3 == 0) {
						engage = true;
					}
					time = 0;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer == 8) {
			isAttacking = false;
			isFloating = false;
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
			gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			gameObject.GetComponent<Animator>().SetBool ("isAttacking", false);
		} else if (other.gameObject.layer == 11 && isAttacking) {
			Debug.Log ("Bitten");
			player.GetComponent<Player_Damage> ().playerCol (rb);
		} else if (other.gameObject.layer == 16) {
			health--;
			if (health == 0) {
				GameObject temp = (GameObject)Instantiate (dead, transform.position, Quaternion.identity);
				temp.SendMessage ("getBombDir", other.transform.localScale.x);
				Destroy (this.gameObject);
			}
		}
	}
}