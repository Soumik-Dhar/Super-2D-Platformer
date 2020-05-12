using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the dust particles effect when the player lands
/// </summary>

public class FeetCTRL : MonoBehaviour 
{
	public GameObject player;

	public Transform dustPosition;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Ground")) 
		{
			sfxCTRL.instance.ShowPlayerLanding (dustPosition.position);

				// Prevents infinite jumping
			gameObject.transform.parent.gameObject.GetComponent<PlayerCTRL> ().isJumping = false;
		}

		if (other.gameObject.CompareTag ("Platform")) 
		{
			sfxCTRL.instance.ShowPlayerLanding (dustPosition.position);

			// Prevents infinite jumping
			gameObject.transform.parent.gameObject.GetComponent<PlayerCTRL> ().isJumping = false;

			player.transform.parent = other.gameObject.transform;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Platform"))
			player.transform.parent = null;
	}
		
}
