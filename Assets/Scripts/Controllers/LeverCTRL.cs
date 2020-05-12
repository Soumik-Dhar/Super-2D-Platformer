using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Releases the dog from the cage
/// </summary>

public class LeverCTRL : MonoBehaviour 
{
	public Vector2 jumpSpeed;

	public GameObject dog;
	public GameObject leverOpen;
	public Transform cage;

	Rigidbody2D rb;

	void Start () 
	{
		rb = dog.GetComponent<Rigidbody2D> ();	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			rb.AddForce (jumpSpeed);

			leverOpen.SetActive (true);
			gameObject.SetActive (false);

			sfxCTRL.instance.ShowEnemyExplosion (cage.position);
			sfxCTRL.instance.ShowPlayerLanding (gameObject.transform.position);
			AudioCTRL.instance.EnemyStomped (gameObject.transform.position);

			Invoke ("ShowLevelCompleteMenu", 2f);
		} 
	}

	void ShowLevelCompleteMenu()
	{
		GameCTRL.instance.LevelComplete ();
	}

}
