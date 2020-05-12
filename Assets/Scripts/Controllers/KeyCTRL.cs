using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Updates the HUD when keys are collected
/// </summary>

public class KeyCTRL : MonoBehaviour 
{
	static int countKey = 1;

	public int keyNumber;
	public float speed;
	public bool startFlying;

	GameObject key;

	void Start()
	{
		startFlying = false;

		if(keyNumber==0)
			key = GameObject.Find ("Image_Key_0");
		else if(keyNumber==1)
			key = GameObject.Find ("Image_Key_1");
		else if(keyNumber==2)
			key = GameObject.Find ("Image_Key_2");
	}

	void Update()
	{
		if (startFlying) 
		{
			transform.position = Vector3.Lerp (transform.position, key.transform.position, speed);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			gameObject.layer = 0;
			startFlying = true;

			PlayerPrefs.SetString ("Key" + countKey, gameObject.name);
			PlayerPrefs.SetInt ("CountKeys",countKey++);

			GameCTRL.instance.UpdateKeyCount (keyNumber);
			sfxCTRL.instance.ShowKeySparkle (keyNumber);
			AudioCTRL.instance.KeyFound (gameObject.transform.position);
		}
	}

}
