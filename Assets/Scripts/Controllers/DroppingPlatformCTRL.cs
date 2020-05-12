using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the platform's dropping behaviour
/// </summary>

public class DroppingPlatformCTRL : MonoBehaviour 
{
	public float droppingDelay;

	Rigidbody2D rb;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Feet"))
			Invoke ("Drop", droppingDelay);
	}

	void Drop()
	{
		rb.isKinematic = false;
	}

}
