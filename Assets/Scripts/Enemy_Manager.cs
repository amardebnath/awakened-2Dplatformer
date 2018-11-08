using UnityEngine;
using System.Collections;

public class Enemy_Manager : MonoBehaviour {

	public void createAnger(int life, Vector3 position)
	{
		
	}

	IEnumerator Create(float wait)
	{
		print ("start");
		yield return new WaitForSeconds (wait);
		//GameObject temp = (GameObject)Instantiate (replacement, transform.position, Quaternion.identity);
	}

}
