using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Activates the bomber bee when the player comes near it
/// </summary>

public class BeeActivator : MonoBehaviour 
{
	public GameObject bee;

	BomberBeeAI bbai;

	void Start () 
	{
		bbai = bee.GetComponent<BomberBeeAI> ();	
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			bbai.ActivateBee (other.gameObject.transform.position);
		}
	}
}
