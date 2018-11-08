using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dl1 : MonoBehaviour {

	public int nextLevel;
	public GameObject canvas, buttons,playerInputs;
	private SpriteRenderer rend;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.CompareTag ("Player"))
		{
			GameController.gameController.Continue = false;
			StartCoroutine(fade(nextLevel));
		}
	}

	IEnumerator fade(int level)
	{
		float alpha = 0f;
		print ("bla");
		rend = canvas.GetComponent<SpriteRenderer> ();
		buttons.SetActive(false);
		playerInputs.GetComponent<Touch_Input>().stopMoving ();
		while (alpha <= 1f) {
			
			alpha += Time.deltaTime / 2.5f;
			rend.color = new Color(0, 0, 0, alpha);
			yield return null;
		}
		Application.LoadLevel (level);
		yield return null;
	}
}
