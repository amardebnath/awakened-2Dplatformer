using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeout : MonoBehaviour {

	public void fadeMe(int level)
	{
		StartCoroutine (fade(level));
	}

	IEnumerator fade(int level)
	{
		CanvasGroup canvasGroup = GetComponent<CanvasGroup> ();
		while (canvasGroup.alpha > 0) {
			canvasGroup.alpha -= Time.deltaTime / .9f;
			yield return null;
		}
		canvasGroup.interactable = false;
		Application.LoadLevel (level);
		yield return null;
	}
}
