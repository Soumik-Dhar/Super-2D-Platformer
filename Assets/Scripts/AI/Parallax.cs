using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the parallax effect
/// </summary>

public class Parallax : MonoBehaviour 
{
	public float speed;
	float offsetX;

	public GameObject player;
	PlayerCTRL playerctrl;

	Material mat;

	void Start () 
	{
		playerctrl = player.GetComponent<PlayerCTRL> ();
		mat=GetComponent<Renderer>().material;
	}

	void Update ()
	{
			// for keyboard/joystick

		if (!playerctrl.isStuck) 
		{
			offsetX += Input.GetAxisRaw ("Horizontal") * speed;

			// for mobile controls

			if (playerctrl.leftPressed)
				offsetX += -speed;
			if (playerctrl.rightPressed)
				offsetX += speed;
			
			mat.SetTextureOffset ("_MainTex", new Vector2 (offsetX, 0));
		}
	}

}

