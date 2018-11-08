using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	public Transform[] angerPos, sadnessPos, fearPos;
	public Transform keyPos;
	private List<GameObject> list = new List<GameObject>();
	public int limit = 10, onScreen = 4, left, angerProb = 2, fearProb = 5, sadnessProb = 3;
	public GameObject temp, anger, sadness, fear, shame, key;
	private bool keyThrown, finished;
	
	void Start(){
		int i;
		for (i = 0; i < onScreen; i++) {
			createEnemy ();
			list.Add (temp);
		}
		left = limit;
	}

	void Update () {
		for (int i = 0; i < onScreen; i++) {
			if (list [i] == null && left - onScreen > 0) {
				createEnemy ();
				list [i] = temp;
				if (list [i] != null) {
					left--;
				}
			}
		}
		finished = true;
		for (int i = 0; i < onScreen; i++)
			if (list [i] != null)
				finished = false;

		if (finished && limit > 0) {
			Vector3 keyPosition;
			keyPosition = keyPos.position;
			if (!keyThrown) {
				keyThrown = true;
				GameObject tempKey = (GameObject)Instantiate (key, keyPosition, Quaternion.identity);
			}
		}
	}

	public void createEnemy()
	{
		int bla = Random.Range (1, 10);
		if (bla % angerProb == 0)
			temp = (GameObject)Instantiate (anger, angerPos [(int)(Random.Range (0, angerPos.Length))].position, Quaternion.identity);
		else if (bla % sadnessProb == 0)
			temp = (GameObject)Instantiate (sadness, sadnessPos [(int)(Random.Range (0, sadnessPos.Length))].position, Quaternion.identity);
		else if (bla % fearProb == 0)
			temp = (GameObject)Instantiate (fear, fearPos [(int)(Random.Range (0, fearPos.Length))].position, Quaternion.identity);
		else
			temp = null;
	}

	public void clearEnemy()
	{
		int i;
		for (i = 0; i < onScreen; i++)
			if(list[i]!=null) Destroy (list [i]);
		list.Clear ();
		for (i = 0; i < onScreen; i++) {
			createEnemy ();
			list.Add (temp);
		}
		left = limit;
	}
}
