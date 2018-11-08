using UnityEngine;
using System.Collections;

public class Fear_Manager : MonoBehaviour {

	public enum State
	{
		Idle,
		Chase,
		Attack,
		Do_nothing
	}

	public GameObject fear;
	public GameObject player;
	public GameObject sphere;
	public float speed;				//veloccity of fear
	public float radius;			//distance which triggers fear into start chasing
	public float offset_x;			//attacking position
	public float offset_y;			//attacking position
	public Animator animator;
	public Vector2 force;
	public Vector3 v;
	public Vector3[] offset = new Vector3[3];
	public bool isAttacking;
	public bool engage;
	public bool byPrincess;
	public State _state;
	public GameObject dead;
	public int health = 3;
	public float throwTime;
	public bool inRange;
	private GameObject princess;
	private Rigidbody2D fearRigidBody;
	public int l;
	private Vector3 pos1, pos2;
	private float x1, x2, y1, y2;
	private float angle, distance, x_per;
	private int i;
	private int bombCount;
	public float time;
	private GameObject tempSphere;
	// Use this for initialization
	void Start () {
		fearRigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		player = GameObject.Find ("Player");
		engage = false;
		_state = State.Idle;
		animator.SetInteger ("movement", 4);
		float offY = Random.Range (6.0f, 12.0f);
		offset [0].y = offY;
		offset [1].y = offY;
		offset [2].y = offY + 4;
		bombCount = 0;
		time = 0;
		if (GameController.gameController.iteration != 0)
			health = 5;

	}


		void FixedUpdate () {
		if (animator.GetLayerWeight (1) == 1f) {
			transform.position += new Vector3((0.1f) * transform.localScale.x, 0.1f, 0f);
			if (animator.GetCurrentAnimatorStateInfo (1).IsName ("New State 0")) {
				animator.SetLayerWeight (1, 0f);
			}
		} else if (!isAttacking) {
			if (inRange) {
				pos1 = transform.position;
				pos2 = player.transform.position + offset [i];
				x_per = (Mathf.Abs (pos1.x - pos2.x)) / (Mathf.Abs (pos1.x - pos2.x) + Mathf.Abs (pos1.y - pos2.y));

				transform.position += new Vector3 (0.03f, 0.03f, 0.0f);

				float d = Mathf.Sqrt ((pos1.x - pos2.x) * (pos1.x - pos2.x) + (pos1.y - pos2.y) * (pos1.y - pos2.y));
				if (d < 0.75f) {
					i++;
					if (i == 3) {
						i = 0;
					}
					if (i == 1) {
						transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
					} else if (i == 2) {
						transform.localScale = new Vector3 (-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
						animator.SetInteger ("movement", 2);
					}
				}
				if (pos1.x < pos2.x) {
					transform.position += new Vector3 (0.2f * x_per, 0f, 0f);
				} else {
					transform.position -= new Vector3 (0.2f * x_per, 0f, 0f);
				}
				if (pos1.y < pos2.y) {
					transform.position += new Vector3 (0f, 0.2f * (1 - x_per), 0f);
				} else {
					transform.position -= new Vector3 (0f, 0.2f * (1 - x_per), 0f);
				}

				time += Time.deltaTime;
				if (time > 1f) {
					int rand = (int)Random.Range (1f, 4f);
					if (rand % 3 == 0) {
						isAttacking = true;
					}
					time = 0;
				}
			} else {
				pos1 = transform.position;
				pos2 = player.transform.position;
				float d = Mathf.Sqrt ((pos1.x - pos2.x) * (pos1.x - pos2.x) + (pos1.y - pos2.y) * (pos1.y - pos2.y));
				if (d < radius) {
					inRange = true;
				}
			}
		} else {
			pos1 = transform.position;
			pos2 = player.transform.position;
			if (pos1.x < pos2.x && transform.localScale.x == 1) {
				transform.localScale = new Vector3 ((transform.localScale.x * -1f), 1f, 1f);
			} else if (pos1.x > pos2.x && transform.localScale.x == -1) {
				transform.localScale = new Vector3 ((transform.localScale.x * -1f), 1f, 1f);
			}
			angle = Mathf.Atan2 (pos1.y - pos2.y, pos1.x - pos2.x);
			angle = angle * (180f / Mathf.PI); 
			if (pos1.x < pos2.x) {
				pos2.x -= offset_x;
			} else {
				pos2.x += offset_x;
			}
			pos2.y += offset_y;

			x1 = pos1.x;
			x2 = pos2.x;
			y1 = pos1.y;
			y2 = pos2.y;

			distance = Mathf.Sqrt ((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
			if (!engage) {
				if (distance < radius) {
					engage = true;
				}
			}

			if (engage) {
				if (distance < 4f) {
					_state = State.Attack;
				} else {
					_state = State.Chase;
				}
			}
			if (_state == State.Idle) {
				idle ();
			} else if (_state == State.Chase) {
				chase ();
			} else if (_state == State.Attack) {
				attack ();
			}
		}
	}
	void idle(){
		_state = State.Chase;
	}
	void chase() {
		transform.position = Vector3.Lerp (pos1, pos2, Time.deltaTime * speed);

		if ((angle >= 0 && angle < 45) || (angle <= 360 && angle > 315)) {
			animator.SetInteger ("movement", 4);
		} else if ((angle >= 45 && angle < 135) && (transform.position.y -10f > player.transform.position.y)) {
			animator.SetInteger ("movement", 3);
		} else if (angle >= 135 && angle < 225) {
			animator.SetInteger ("movement", 4);
		} else if ((angle >= 225 && angle < 315) && transform.position.y +10f < player.transform.position.y) {
			animator.SetInteger ("movement", 2);
		}
	}


	void attack () {
		animator.SetInteger ("movement", 6);
		transform.position = Vector3.Lerp (pos1, pos2, Time.deltaTime * speed);
		time += Time.deltaTime;
		if (time > throwTime) {
			tempSphere = (GameObject)Instantiate (sphere, transform.position, Quaternion.identity);
			tempSphere.SendMessage ("getPlayerTransform", player.transform.position);
			tempSphere.SendMessage ("getFearScale", fear.transform.localScale.x);
			time = 0;
			bombCount++;
			if (bombCount > 1) {
				isAttacking = false;
				bombCount = 0;
				time = 0;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Bomb")){
			health--;
			if (health == 0) {
				
					GameObject temp = (GameObject)Instantiate (dead, transform.position, Quaternion.identity);
					temp.SendMessage ("getBombDir", other.transform.localScale.x);

					if (byPrincess) {
						princess = GameObject.Find ("Princess");
						princess.GetComponent<PrincessFinal> ().enemyDied ();
					}
					Destroy (fear);

			}
			animator.SetLayerWeight (1, 1f);
			//fearRigidBody.AddForce(force*2, ForceMode2D.Impulse);
		}
	}
		
}