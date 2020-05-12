using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// AI engine for the bomber bee
/// </summary>

public class BomberBeeAI : MonoBehaviour 
{
	public float speed;

	public void ActivateBee(Vector3 playerPos)
	{
		transform.DOMove (playerPos, speed, false);	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player") || other.gameObject.CompareTag ("Ground")) 
		{
			sfxCTRL.instance.ShowEnemyExplosion (other.gameObject.transform.position);

			Destroy (gameObject);
		}
	}

}
