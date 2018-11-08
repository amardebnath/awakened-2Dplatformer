using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class Intializee : MonoBehaviour {

	void Start () {
		GameController.gameController.mainMenu = true;
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			//GameController.gameController.Load ();
			GameController.gameController.mainMenu = true;
			Application.LoadLevel (GameController.gameController.currentLevel);
		} else {
			GameController.gameController.Continue = false;
			Application.LoadLevel (1);
		}
	}

}
