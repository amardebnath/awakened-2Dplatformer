using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameController : MonoBehaviour {

	public static GameController gameController;
	public float playerPositionX, playerPositionY;
	public int currentLevel=1, enemyManager, PI, iteration=0;
	public bool mainMenu, pauseMenu,Continue;
	public float playerDamage = 100f;

	void Awake()
	{
		if (gameController == null) {
			DontDestroyOnLoad (gameObject);
			gameController = this;
		} else if (gameController != this)
			Destroy (gameObject);
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData ();
		data.playerPosX = playerPositionX;
		data.playerPosY = playerPositionY;
		data.playerDamage = playerDamage;
		data.currentLevel = currentLevel;
		data.mainMenu = mainMenu;
		data.pauseMenu = pauseMenu;
		data.Continue = Continue;
		data.enemyManager = enemyManager;
		data.PI = PI;
		data.iteration = iteration;
		//print (PI);
		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			playerPositionX = data.playerPosX;
			playerPositionY = data.playerPosY;
			playerDamage = data.playerDamage;
			currentLevel = data.currentLevel;
			mainMenu = data.mainMenu;
			Continue = data.Continue;
			enemyManager = data.enemyManager;
			PI = data.PI;
			iteration = data.iteration;

		} else {
			
			playerPositionX = -49.4f;
			playerPositionY = -54.4f;
			playerDamage = 100;
			currentLevel = 1;
			PI = 0;
			iteration = 0;
			mainMenu = false;
			pauseMenu = false;
			Continue = false;
			Save ();
			Load ();

		}
	}

	public void Delete()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			File.Delete (Application.persistentDataPath + "/playerInfo.dat");
		}
	}
}

[Serializable]
class PlayerData
{
	public float playerPosX;
	public float playerDamage, playerPosY;
	public int  currentLevel, enemyManager, PI, iteration;
	public bool mainMenu, pauseMenu, Continue;
}
