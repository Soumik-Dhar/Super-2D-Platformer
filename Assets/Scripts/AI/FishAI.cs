using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI engine for the jumping fish
/// </summary>

public class FishAI : MonoBehaviour 
{
	public float jumpBoost;

	Rigidbody2D rb;
	SpriteRenderer sr;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();

		Jump ();
	}

	void Update () 
	{
		if (rb.velocity.y > 0)
			sr.flipY = false;
		else
			sr.flipY = true;
	}

	public void Jump()
	{
		rb.AddForce (new Vector2 (0, jumpBoost));
	}

}
