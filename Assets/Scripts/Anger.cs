using UnityEngine;
using System.Collections;

public class Anger : MonoBehaviour {

	public float stepTime;				//time of each step of cycle
	public float range = 6;						//distance from player
	public float run_speed = 10f;
	public bool isRunning = false, front_line, back_line;
	private Rigidbody2D angerRigidBody;
	public Transform front_start, front_end, back_start, back_end;
	public GameObject player;
	public GameObject anger;
	public GameObject dead;
	public bool byPrincess;
	Enemy_Health eHealth;
	private Animator animator;
	public int dir = -1, health = 3, temp;
	private int i = 0;
	private float runRangeX;
	private float time = 0;
	private bool isFirst = true;

	private bool isInFinal;

	void Start () {
		angerRigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	
		player = GameObject.Find ("Player");
	
		if (GameController.gameController.iteration != 0)
			health = 5;

	}

	void Update () {
		front_line = Physics2D.Linecast(front_start.position, front_end.position, 1 << LayerMask.NameToLayer("Player"));
		back_line = Physics2D.Linecast(back_start.position, back_end.position, 1 << LayerMask.NameToLayer("Player"));

		if (front_line) {
			if (i == 0) {
				angerRigidBody.velocity = new Vector2 (dir * run_speed, 0f);
				animator.SetBool ("isRunning", true);
				if (isFirst) {
					runRangeX = player.transform.position.x + dir * range;
					time = 0;
					isFirst = false;
				} else {
					if (dir == 1) {
						if (transform.position.x > runRangeX) {
							animator.SetBool ("isRunning", false);
							i++;
							isFirst = true;
						}
					} else {
						if (transform.position.x < runRangeX) {
							animator.SetBool ("isRunning", false);
							i++;
							isFirst = true;
						}
					}
				}
			} else {
				time += Time.deltaTime;
				if (time > stepTime) {
					i++;
					if (i == 3) {
						i = 0;
					}
					time = 0;
				}
			}
		} else if (back_line) {
			dir = dir * -1;
			transform.localScale = new Vector3 (-1 * dir * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
			angerRigidBody.velocity = new Vector2 (dir * run_speed, 0f);
		} else {
			animator.SetBool ("isRunning", false);
		}

		if (health < temp) {
			animator.SetTrigger ("enemy_hurt");
			temp = health;
		}
		if (health == 0) {
			GameObject dead_anger = (GameObject)Instantiate (dead, transform.position, Quaternion.identity);
			dead_anger.SendMessage ("getBombDir", player.transform.localScale.x);
			if (byPrincess) {
				GameObject princess = GameObject.Find ("Princess");	
				princess.GetComponent<PrincessFinal> ().enemyDied ();
			}
			Destroy (anger);
		}
	}

	public void enemy_hit()
	{
		transform.localScale = new Vector3 (-1 * dir * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
		angerRigidBody.velocity = new Vector2 (-1 * dir * 3f, 0f);
	}

}