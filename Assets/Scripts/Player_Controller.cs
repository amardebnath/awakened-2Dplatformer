using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Controller : MonoBehaviour {

	public float runSpeed = 10f, scale = 0.6f, airSpeed = 1f, moving, slidingSpeed = -0.5f, onAirHorizontalSpeed=8.5f, prev = 1000f;
	public Transform GC1s, GC1e, GC2s, GC2e, GC3s, GC3e, WC1s, WC1e, WC2s, WC2e, PBs, PBe;
	public float jumpHeight = 30f, wallJumpY = 20f, wallJumpX = 4.5f, dir=1;
	public bool onGround, onWall, PB, movable = true;
	public int jumpCount = 0;
	public GameObject bomb;
	private int layer;
	public AudioClip leftFoot, rightFoot, land;
	public AudioSource audio;

	/*------------*/ 
	public bool isBlue, isRed, isYellow;

	public Rigidbody2D player_rigidbody;
	public Animator animator;
	public bool wallIdle = false;
	private bool GC1, GC2, GC3, WC1, WC2, pre = true;

	/*------------*/


	void Start(){
		audio = this.GetComponent<AudioSource> ();
	}

	void Update () {
		
		layer = (1 << LayerMask.NameToLayer ("Ground")) | (1 << LayerMask.NameToLayer ("NonWall"));
		GC1 = Physics2D.Linecast(GC1s.position, GC1e.position, layer);
		GC2 = Physics2D.Linecast(GC2s.position, GC2e.position, layer);
		GC3 = Physics2D.Linecast(GC3s.position, GC3e.position, layer);
		WC1 = Physics2D.Linecast(WC1s.position, WC1e.position, 1 << LayerMask.NameToLayer("Ground"));
		WC2 = Physics2D.Linecast(WC2s.position, WC2e.position, 1 << LayerMask.NameToLayer("Ground"));
		PB = Physics2D.Linecast(PBs.position, PBe.position, 1 << LayerMask.NameToLayer("Push_Block"));
		Debug.DrawLine(WC1s.position, WC1e.position, Color.green);

		onGround = (GC1 || GC2 || GC3);
		onWall = (WC1 || WC2);
		animator.SetBool ("onGround", true);
		//if(movable) moving = Input.GetAxisRaw("Horizontal");

		if (!onWall)
			fallFromWall ();

		if (onGround)
			animator.SetBool ("onGround", true);
		else
			animator.SetBool ("onGround", false);
		if (moving != 0)
			animator.SetBool ("moving", true);
		else
			animator.SetBool ("moving", false);


		if (moving != 0) {
			if (wallIdle) {
				animator.SetBool ("moving", false);
				//moving = 0;
			}
			else {
				dir = moving;
				transform.localScale = new Vector3 (dir * scale, scale, 1);
				if (onGround) {
					player_rigidbody.velocity = new Vector2 (dir * runSpeed, player_rigidbody.velocity.y);
				}
				else if(!onGround)
					player_rigidbody.velocity = new Vector2 (dir * onAirHorizontalSpeed, player_rigidbody.velocity.y);
			}
		}

		if (onGround) {
			prev = 1000f;
			wallIdle = false;
			if (player_rigidbody.velocity.y < 0.1) {
				jumpCount = 0;
			}
			animator.SetBool ("jumpingDown", false);
			animator.SetBool ("wallIdle", false);
			if (PB && moving != 0)
				animator.SetBool ("Push", true);
			else
				animator.SetBool("Push", false);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			jump ();
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			bombAttack ();
		}


		if (player_rigidbody.velocity.y < 0.1f) {
			animator.SetBool ("jumpingUp", false);
			if (!wallIdle)
				animator.SetBool ("jumpingDown", true);
			else player_rigidbody.velocity = new Vector2 (0f, slidingSpeed);
		}

		if(WC1 && !onGround && player_rigidbody.velocity.y < 0.5f){
			if(!wallIdle && ((Mathf.Abs(prev - transform.position.x) > 2f))) {
				animator.SetBool ("jumpingUp", false);
				animator.SetBool ("jumpingDown", false);
				animator.SetBool ("wallIdle", true);
				prev = transform.position.x;
				wallIdle = true;
				jumpCount = 0;
				dir = dir * -1;
				transform.localScale = new Vector3 (dir * scale, scale, 1);
			}
		}
	}

	void throwBomb()
	{
		GameObject temp = (GameObject)Instantiate (bomb, transform.position, Quaternion.identity);
		temp.GetComponent<Bomb>().initialize(dir);
		if(wallIdle) temp.transform.position = new Vector3 (temp.transform.position.x + dir * 1.2f, transform.position.y, 1);

	}

	public void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.CompareTag("key")){
			if(col.gameObject.GetComponent<key>().color == 1){
				isBlue = true;
			} else if(col.gameObject.GetComponent<key>().color == 2){
				isRed = true;
			} else {
				isYellow = true;
			}
			Destroy(col.gameObject);
		}
	}

	public void jump()
	{
		if (!wallIdle && jumpCount < 2) {
			jumpCount++;
			player_rigidbody.velocity = new Vector2 (player_rigidbody.velocity.x, jumpHeight);
			animator.SetBool ("jumpingUp", true);
			animator.SetBool ("jumpingDown", false);
		} else if (wallIdle && jumpCount < 2) {
			jumpCount++;
			wallIdle = false;
			animator.SetBool ("wallIdle", false);
			player_rigidbody.velocity = new Vector2 (dir * wallJumpX, wallJumpY);
			animator.SetBool ("jumpingDown", true);
		}
	}

	public void bombAttack()
	{
		if (onGround) {
			moving = 0;
			player_rigidbody.velocity = new Vector2 (0, player_rigidbody.velocity.y);
			animator.SetTrigger ("bombThrow");
		} else if (wallIdle)
			animator.SetTrigger ("wallBombThrow");
		else
			animator.SetTrigger ("jumpBombThrow");
	}

	public void moveRight()
	{
		if(movable) moving = 1;
	}

	public void moveLeft()
	{
		if(movable) moving = -1;
	}

	public void stopMoving()
	{
		moving = 0;
	}
	public void playerHurt()
	{
		transform.localScale = new Vector3 (dir * scale, scale, 1);
		player_rigidbody.velocity = new Vector2 (-1 * dir * 10f, player_rigidbody.velocity.y);
	}
	public void fallFromWall()
	{
		wallIdle = false;
		animator.SetBool ("wallIdle", false);
	}

	public void leftFootSound()
	{
		audio.clip = leftFoot;
		audio.Play ();
	}

	public void rightFootSount()
	{
		audio.clip = rightFoot;
		audio.Play ();
	}

}

