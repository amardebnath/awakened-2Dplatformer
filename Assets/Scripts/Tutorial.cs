using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	public GameObject tutScreen, controls, stopMove;
	public float waitTime;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			StartCoroutine (tellStory (waitTime));

		}
	}

	IEnumerator tellStory(float waitTime)
	{
		tutScreen.SetActive (true);
		controls.SetActive (false);
		stopMove.GetComponent<Touch_Input>().stopMoving ();
		yield return new WaitForSeconds(waitTime);
		CanvasGroup canvasGroup = tutScreen.GetComponent<CanvasGroup> ();
		while (canvasGroup.alpha > 0) {
			canvasGroup.alpha -= Time.deltaTime / 1.1f;
			yield return null;
		}
		canvasGroup.interactable = false;
		tutScreen.SetActive(false);
		controls.SetActive (true);
		gameObject.SetActive (false);
	}
}
