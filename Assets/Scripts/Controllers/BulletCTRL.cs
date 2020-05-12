using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls bullet physics
/// </summary>

public class BulletCTRL : MonoBehaviour 
{
	public Vector2 velocity;
	Rigidbody2D rb;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update()
	{
		rb.velocity = velocity;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Enemies") || other.gameObject.CompareTag ("Monster")) 
		{
			GameCTRL.instance.BulletHitEnemy (other.gameObject.transform);
			Destroy (gameObject);
		}

		else if (!other.gameObject.CompareTag ("Player"))
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Enemies")) 
		{
			GameCTRL.instance.BulletHitEnemy (other.gameObject.transform);
			Destroy (gameObject);
		}
	}

}
