using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignCTRL : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			GameCTRL.instance.ActivateEnemySpawner ();
			GameCTRL.instance.timerOn = false;
			LevelOneBossAI.instance.life.SetActive (true);

			Destroy (gameObject);
		}
	}

}
