using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class MenuInput : MonoBehaviour {

	public GameObject pauseMenu, controlButtons, canvas;
	public Texture2D fadeimage;

	public void newGame()
	{
		GameController.gameController.mainMenu = false;
		GameController.gameController.Continue = false;
		canvas.GetComponent<fadeout> ().fadeMe (1);
	}

	public void Continue()
	{
		if (!File.Exists (Application.persistentDataPath + "/playerInfo.dat"))
			return;
		GameController.gameController.Load ();
		GameController.gameController.mainMenu = false;
		GameController.gameController.Continue = true;
		print (GameController.gameController.playerPositionX);
		canvas.GetComponent<fadeout> ().fadeMe (GameController.gameController.currentLevel);
	}

	public void credits()
	{
		canvas.GetComponent<fadeout> ().fadeMe (8);
	}

	public void MainMenu()
	{
		Time.timeScale = 1;
		GameController.gameController.mainMenu = true;
		Application.LoadLevel (0);
	}
		
	public void Pause()
	{
		Time.timeScale = 0;
		controlButtons.SetActive (false);
		pauseMenu.SetActive (true);
	}

	public void Resume()
	{
		Time.timeScale = 1;
		controlButtons.SetActive (true);
		pauseMenu.SetActive (false);
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void backMenu()
	{
		canvas.GetComponent<fadeout> ().fadeMe (0);
	}
}
