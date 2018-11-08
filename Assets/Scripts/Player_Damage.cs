using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;

public class Player_Damage : MonoBehaviour {

	public float damageCount = 100f, maxHealth = 100f;
	public Text damageText;
	public Image healthBar;
	private Renderer rend;
	public GameObject CentralManager;
	private CentralEnemy ce;
	private GameObject player, princess;
	public AudioSource audio;
	public AudioClip land;

	void Start () {
		audio = GetComponent<AudioSource> ();
		CentralManager = GameObject.Find ("CentralEnemyManager");
		ce = CentralManager.GetComponent<CentralEnemy> ();
		rend = GetComponent<Renderer> ();
		if (GameController.gameController.Continue == true) {
			damageCount = GameController.gameController.playerDamage;
			transform.position = new Vector3(
				GameController.gameController.playerPositionX,
				GameController.gameController.playerPositionY,
				1);	
		}
	}


	void Update () {

		healthBar.transform.localScale = new Vector3 (damageCount / maxHealth, 1, 1);
		if (damageCount <= 0) {
			int i = GameController.gameController.enemyManager;
			ce.manager [i].GetComponent<EnemyManager> ().clearEnemy ();
			ce.manager [i].GetComponent<EnemyManager> ().left = ce.manager [i].GetComponent<EnemyManager> ().limit;
			GameController.gameController.Load ();
			print (transform.position);
			transform.position = new Vector3 (
				GameController.gameController.playerPositionX,
				GameController.gameController.playerPositionY, 
				1
			);
			damageCount = GameController.gameController.playerDamage;
		}
	}

	public void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.layer == 14)
		{
			playerCol (col.gameObject.GetComponent<Rigidbody2D> ());
		}
		/*if (col.gameObject.layer == 8)
		{
			if((Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < 0.1f) && (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < 0.1f)){
				landSound();
			}
		}*/
	}

	public void statueTouched(int index)
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		princess = GameObject.FindGameObjectWithTag("Princess");
		print ("Princess");
		if (princess != null) {
			
			GameController.gameController.PI = princess.GetComponent<PrincessMovement> ().i;

		}
		//print (princess.GetComponent<PrincessMovement> ().i + " " + GameController.gameController.PI);
		GameController.gameController.playerPositionX = player.transform.position.x;
		GameController.gameController.playerPositionY = player.transform.position.y;
		GameController.gameController.playerDamage = damageCount;
		GameController.gameController.mainMenu = false;
		GameController.gameController.Continue = true;
		GameController.gameController.currentLevel = Application.loadedLevel;
		GameController.gameController.enemyManager = index;
		GameController.gameController.Save ();
	}

	IEnumerator DoBlinks(int blinkCount, float blinkTime) {
		gameObject.layer = 13;
		while (2*blinkCount > 0f) {
			blinkCount -= 1;

			rend.enabled = !rend.enabled;
			yield return new WaitForSeconds(blinkTime);
		}
		rend.enabled = true;
		gameObject.layer = 11;
	}

	public void playerCol(Rigidbody2D rb)
	{
		damageCount -= 10f;

		gameObject.GetComponent<Animator> ().SetTrigger ("playerHurt");
		if (rb.velocity.x < 0f)
			gameObject.GetComponent<Player_Controller> ().dir = 1;
		else
			gameObject.GetComponent<Player_Controller> ().dir = -1;
		StartCoroutine (DoBlinks (10, .12f));
	}

	public void landSound()
	{
		audio.clip = land;
		audio.Play ();
	}
}
