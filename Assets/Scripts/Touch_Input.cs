using UnityEngine;
using System.Collections;

public class Touch_Input : MonoBehaviour {

	public Player_Controller player;

	public void jump()
	{
		player.jump ();
	}

	public void bombAttack()
	{
		player.bombAttack ();
	}

	public void moveRight()
	{
		player.moveRight ();
	}

	public void moveLeft()
	{
		player.moveLeft ();
	}

	public void stopMoving()
	{
		player.stopMoving ();
	}
}
