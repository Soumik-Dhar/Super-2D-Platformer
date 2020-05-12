using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// AI engine for the level one boss
/// </summary>

public class LevelOneBossAI : MonoBehaviour 
{
	public static LevelOneBossAI instance;

	public float jumpBoost;
	public float delayFire;
	public int startJumpingAt;
	public int jumpDelay;
	public int health;

	public Slider healthBar;
	public GameObject bossBullet;
	public GameObject life;


	Rigidbody2D rb;
	SpriteRenderer sr;

	Vector3 bulletSpawnPos;
	bool canFire,isJumping;

	void Start () 
	{
		if (instance == null)
			instance = this;
		
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();

		canFire = false;

		bulletSpawnPos = gameObject.transform.Find ("BulletSpawnPos").transform.position;

		Invoke ("Reload", Random.Range (1f, delayFire));
	}

	void Update () 
	{
		if (canFire) 
		{
			FireBullets ();
			canFire = false;

			if (health < startJumpingAt && !isJumping) 
			{
				InvokeRepeating ("Jump", 0, jumpDelay);
				isJumping=true;
			}
		}	
	}

	void Reload()
	{
		canFire = true;
	}

	void Jump()
	{
		rb.AddForce (new Vector2 (0, jumpBoost));
	}

	void FireBullets()
	{
		Instantiate (bossBullet, bulletSpawnPos, Quaternion.identity);

		Invoke ("Reload", delayFire);
	}

	void RestoreColor()
	{
		sr.color = Color.white;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("PlayerBullet")) 
		{
			if (health == 0) 
			{
				GameCTRL.instance.BulletHitEnemy (gameObject.transform);

				Destroy (life);
			}

			if (health > 0) 
			{
				health--;
				healthBar.value = (float)health;

				sr.color = Color.red;

				Invoke ("RestoreColor", 0.1f);
			}
		}
	}

}
