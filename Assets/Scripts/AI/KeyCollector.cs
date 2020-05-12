using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Receives flying keys after pickup
/// </summary>

public class KeyCollector : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Key"))
			Destroy (other.gameObject);
	}

}
