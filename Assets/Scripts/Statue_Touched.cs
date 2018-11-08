using UnityEngine;
using System.Collections;

public class Statue_Touched : MonoBehaviour {

	public GameObject player, dScreen, buttons, stopMove;
	private bool touched = false;
	public int index;
	public bool story;
	public float waitingTime = 6f;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			print ("touch");
			if (touched == false) {
				touched = true;

				if (story) {
					StartCoroutine (tellStory (waitingTime));
				}
				player.GetComponent<Player_Damage>().statueTouched (index);
				print ("saved");
				if(this.GetComponent<ParticleSystem> () != null)
					this.GetComponent<ParticleSystem> ().Stop ();
			}
		}
	}

	IEnumerator tellStory(float waitTime)
	{
		dScreen.SetActive (true);
		buttons.SetActive (false);
		stopMove.GetComponent<Touch_Input>().stopMoving ();
		yield return new WaitForSeconds(waitTime);
		CanvasGroup canvasGroup = dScreen.GetComponent<CanvasGroup> ();
		while (canvasGroup.alpha > 0) {
			canvasGroup.alpha -= Time.deltaTime / 1.1f;
			yield return null;
		}
		canvasGroup.interactable = false;
		dScreen.SetActive(false);
		buttons.SetActive (true);
	}
}
