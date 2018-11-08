using UnityEngine;
using System.Collections;

public class LoadMenu : MonoBehaviour {
	public GameObject MainMenu, PauseMenu, ControlButtons;
	void Start () {
		if (GameController.gameController.mainMenu == true) {
			MainMenu.SetActive (true);
			ControlButtons.SetActive (false);
		}
	}

}
