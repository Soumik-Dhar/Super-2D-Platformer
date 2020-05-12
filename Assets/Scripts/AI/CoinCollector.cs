using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Receives flying coins after pickup
/// </summary>

public class CoinCollector : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Coin") || other.gameObject.CompareTag ("ShiningCoin"))
			Destroy (other.gameObject);
	}

}
