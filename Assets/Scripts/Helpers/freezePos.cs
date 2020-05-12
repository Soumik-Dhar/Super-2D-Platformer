using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezePos : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			GameCTRL.instance.StopCameraFollow ();
			Destroy (gameObject);
		}
	}

}
