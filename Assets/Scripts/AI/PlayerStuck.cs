using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks when player is stuck
/// </summary>

public class PlayerStuck : MonoBehaviour 
{
	public GameObject player;

	PlayerCTRL playerctrl;

	void Start () 
	{
		playerctrl = player.GetComponent<PlayerCTRL> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		playerctrl.isStuck = true;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		playerctrl.isStuck = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		playerctrl.isStuck = false;
	}

}
