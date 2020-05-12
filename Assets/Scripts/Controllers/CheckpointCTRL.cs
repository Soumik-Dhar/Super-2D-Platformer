using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Saves the current player position in PlayerPrefs
/// </summary>

public class CheckpointCTRL : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			PlayerPrefs.SetFloat ("CPX", other.gameObject.transform.position.x);
			PlayerPrefs.SetFloat ("CPY", other.gameObject.transform.position.y);
		}
	}

}
