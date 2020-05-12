using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages -
/// 1. Movement options for the Player object Cat
/// 2. Animation options for the Player object Cat
/// </summary>

public class PlayerCTRL : MonoBehaviour 
{
	public static PlayerCTRL instance;

	[Tooltip("A postitive integer which increases player speed")]
	public int speedBoost;
	[Tooltip("A postitive integer which sets vertical speed")]
	bool canDoubleJump;

	public bool leftPressed,rightPressed;
	public bool isGrounded;
	public bool isJumping;
	public bool sfxOn;
	public bool canFire;
	public bool isStuck;

	public float jumpSpeed;
	public float boxwidth, boxheight;
	public float delayForDoubleJump;

	public Transform feet, LeftBulletSpawnPos, RightBulletSpawnPos;

	public GameObject LeftBullet, RightBullet;
	public GameObject garbageCTRL;

	public LayerMask whatIsGround;

	Rigidbody2D rb;
	SpriteRenderer sr;
	Animator anim;

	void Awake()
	{
		if(PlayerPrefs.HasKey("CPX"))
		{
			transform.position = new Vector3 (PlayerPrefs.GetFloat ("CPX"), PlayerPrefs.GetFloat ("CPY"), transform.position.z);
		}
	}

	void Start () 
	{
		rb = GetComponent<Rigidbody2D> (); 
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();

		if (instance == null)
			instance = this;

		if (PlayerPrefs.GetInt ("BPU") == 1)
			canFire = true;
	}

	void Update () 
	{
		isGrounded = Physics2D.OverlapBox (new Vector2 (feet.position.x,feet.position.y), new Vector2 (boxwidth, boxheight), 360.0f, whatIsGround);

		float playerSpeed = Input.GetAxisRaw ("Horizontal");
		playerSpeed *= speedBoost;

		if (playerSpeed != 0)
			MoveHorizontal (playerSpeed);
		else
			Stop ();

		if (Input.GetButtonDown ("Jump"))
			Jump ();

		if (Input.GetButtonDown ("Fire1"))
			FireBullets ();

		Fall ();

		if (leftPressed)
			MoveHorizontal (-speedBoost);
		if (rightPressed)
			MoveHorizontal (speedBoost);
		
	}
		
		// Gizmos for groundcheck

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (feet.position, new Vector3 (boxwidth, boxheight,0));
	}

		// Moving the player left and right

	void MoveHorizontal (float playerSpeed) 
	{
		rb.velocity = new Vector2 (playerSpeed, rb.velocity.y);

		if (playerSpeed < 0)
			sr.flipX = true;
		else
			sr.flipX = false;

		if(!isJumping)
			anim.SetInteger ("State", 1);
	}

		// Stopping the player

	void Stop () 
	{
		rb.velocity = new Vector2 (0, rb.velocity.y);

		if(!isJumping)
			anim.SetInteger ("State", 0);
	}

		// Freezing the player in space

	public void freeze()
	{
		rb.constraints = RigidbodyConstraints2D.FreezePosition;
	}

		// Making the player Jump

	void Jump () 
	{
		if (!isJumping && isGrounded) 
		{
			isJumping = true;
			rb.AddForce(new Vector2 (0,jumpSpeed));
			anim.SetInteger ("State", 2);

			AudioCTRL.instance.PlayerJump (gameObject.transform.position);

			Invoke ("EnableDoubleJump", delayForDoubleJump);
		}

		if (canDoubleJump && !isGrounded) 
		{
			rb.velocity = Vector2.zero;
			rb.AddForce(new Vector2 (0,jumpSpeed));
			anim.SetInteger ("State", 2);

			AudioCTRL.instance.PlayerJump (gameObject.transform.position);

			canDoubleJump = false;
		}
	}

		// Making the player jump twice 

	void EnableDoubleJump()
	{
		canDoubleJump = true;
	}
		
		// Setting falling animation

	void Fall()
	{
		if (rb.velocity.y < 0)
			anim.SetInteger ("State", 3);
	}

		// Making the player fire bullets

	void FireBullets()
	{
		if (canFire) 
		{
			if(sr.flipX)
				Instantiate (LeftBullet, LeftBulletSpawnPos.position, Quaternion.identity);
			if(!sr.flipX)
				Instantiate (RightBullet, RightBulletSpawnPos.position, Quaternion.identity);

			AudioCTRL.instance.FireBullets (gameObject.transform.position);
		}
	}

		// UI controls for mobile platform

	public void MobileMoveLeft()
	{
		leftPressed = true;
	}

	public void MobileMoveRight()
	{
		rightPressed = true;
	}

	public void MobileJump()
	{
		Jump ();
	}

	public void MobileStop()
	{
		leftPressed = false;
		rightPressed = false;
		Stop ();
	}

	public void MobileFireBullets()
	{
		FireBullets ();	
	}

		// Making the coins sparkle

	void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.gameObject.tag) 
		{
		case "Coin":

			if (sfxOn)
				sfxCTRL.instance.ShowCoinSparkle (other.gameObject.transform.position);

			GameCTRL.instance.UpdateCoinCount ();
			GameCTRL.instance.UpdateScore (GameCTRL.Item.Coin);
			AudioCTRL.instance.CoinPickup (gameObject.transform.position);
			break;

		case "BulletPowerUp":

			canFire = true;

			PlayerPrefs.SetInt ("BPU", 1);
			Destroy (other.gameObject);

			if (sfxOn)
				sfxCTRL.instance.ShowBulletSparkle (other.gameObject.transform.position);

			AudioCTRL.instance.PowerUp (gameObject.transform.position);
			break;

		case "Water":

			garbageCTRL.SetActive (false);

			if (sfxOn)
				sfxCTRL.instance.ShowSplash (gameObject.transform.position);

			GameCTRL.instance.PlayerDrowned (other.gameObject);
			AudioCTRL.instance.WaterSplash (gameObject.transform.position);
			break;

		case "Enemies":
			
			anim.SetInteger ("State", -1);

			GameCTRL.instance.PlayerDeathAnimation (gameObject);
			AudioCTRL.instance.PLayerDied (gameObject.transform.position);
			break;

		case "BossKey":

			GameCTRL.instance.ShowLever ();
			GameCTRL.instance.DeactivateEnemySpawner ();

			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Monster"); 
			foreach (GameObject enemy in enemies) 
			{
				sfxCTRL.instance.ShowPlayerLanding (enemy.transform.position);
				AudioCTRL.instance.EnemyStomped (enemy.transform.position);
				Destroy (enemy);
			}
			break;

		default:
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Enemies") || other.gameObject.CompareTag ("JumpingFish") || other.gameObject.CompareTag ("LevelOneBoss") || other.gameObject.CompareTag ("Monster")) 
		{
			anim.SetInteger ("State", -1);

			GameCTRL.instance.StopCameraFollow ();
			GameCTRL.instance.PlayerDeathAnimation (gameObject);
			AudioCTRL.instance.PLayerDied (gameObject.transform.position);
		}

		if (other.gameObject.CompareTag ("ShiningCoin")) 
		{
			if (sfxOn)
				sfxCTRL.instance.ShowBulletSparkle (other.gameObject.transform.position);

			GameCTRL.instance.UpdateCoinCount ();
			GameCTRL.instance.UpdateScore (GameCTRL.Item.ShiningCoin);
			AudioCTRL.instance.CoinPickup (gameObject.transform.position);
		}
	}
		
}

