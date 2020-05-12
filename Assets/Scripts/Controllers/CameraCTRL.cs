using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls camera movements
/// </summary>

public class CameraCTRL : MonoBehaviour 
{
	public static CameraCTRL instance;
	public Transform player;

	public float yOffset;

	void Start()
	{
		if (instance == null)
			instance = this;
	}

	void Update () 
	{
		transform.position = new Vector3 (player.position.x, transform.position.y, transform.position.z);
	}

}
