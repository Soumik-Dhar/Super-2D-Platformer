using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys game objects except the player 
/// Restarts the level if the player dies
/// </summary>

public class GarbageCTRL : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			GameCTRL.instance.PlayerDied (other.gameObject);
		}
		else
		{
			Destroy(other.gameObject);	
		}
	}
}
