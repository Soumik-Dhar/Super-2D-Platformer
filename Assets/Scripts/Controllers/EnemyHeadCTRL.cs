using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls enemy behaviour when player jumps on top of it
/// </summary>

public class EnemyHeadCTRL : MonoBehaviour 
{
	public GameObject enemy;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Feet")) 
		{
			GameCTRL.instance.PlayerStompsEnemy (enemy);

			sfxCTRL.instance.ShowEnemyExplosion (enemy.transform.position);
		}
	}
}
