using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls coin physics
/// </summary>

public class CoinCTRL : MonoBehaviour
{
	static int countCoin = 1;

	public enum CoinFX
	{
		Vanish,
		Fly
	}

	public CoinFX coinFX;
	public float speed;
	public bool startFlying;

	GameObject coinMeter;

	void Start()
	{
		startFlying = false;

		if (coinFX == CoinFX.Fly)
			coinMeter = GameObject.Find ("Image_Coin_Count");
	}

	void Update()
	{
		if (startFlying) 
		{
			transform.position = Vector3.Lerp (transform.position, coinMeter.transform.position, speed);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			if (coinFX == CoinFX.Vanish) 
			{	
				Destroy (gameObject);
			}

			else if (coinFX == CoinFX.Fly) 
			{
				gameObject.layer = 0;
				startFlying = true;
			}

			PlayerPrefs.SetString ("Coin" + countCoin, gameObject.name);
			PlayerPrefs.SetInt ("CountCoins",countCoin++);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			if (coinFX == CoinFX.Vanish) 
			{	
				Destroy (gameObject);
			}

			else if (coinFX == CoinFX.Fly) 
			{
				gameObject.layer = 0;
				startFlying = true;
			}
		}
	}

}
