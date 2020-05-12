using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement options for the Green Monster enemy
/// </summary>

public class EnemyPatrolCTRL : MonoBehaviour 
{
	public Transform LeftEdge, RightEdge;
	public float minDelay, maxDelay;
	public float speed;

	float originalSpeed;
	bool canTurn;

	Rigidbody2D rb;
	SpriteRenderer sr;
	Animator anim;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();

		SetStartingDirection ();

		canTurn = true;
	}

	void Update () 
	{
		Move ();	
		FlipAtEdge ();
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawLine (LeftEdge.position, RightEdge.position);
	}

	void Move()
	{
		Vector2 temp = rb.velocity;
		temp.x = speed;
		rb.velocity = temp;
	}

	void SetStartingDirection()
	{
		if (speed > 0)
			sr.flipX = true;
		else if (speed < 0)
			sr.flipX = false;
	}

	void FlipAtEdge()
	{
		if (sr.flipX && (transform.position.x >= RightEdge.position.x)) 
		{
			if (canTurn) 
			{
				canTurn = false;
				originalSpeed = speed;
				speed = 0;

				StartCoroutine ("TurnLeft", originalSpeed);
			}
		}

		else if (!sr.flipX && (transform.position.x <= LeftEdge.position.x)) 
		{
			if (canTurn) 
			{
				canTurn = false;
				originalSpeed = speed;
				speed = 0;

				StartCoroutine ("TurnRight", originalSpeed);
			}
		}
	}

	IEnumerator TurnLeft(float originalSpeed)
	{
		anim.SetBool ("isIdle", true);
		yield return new WaitForSeconds (Random.Range (minDelay, maxDelay));
		anim.SetBool ("isIdle", false);
		sr.flipX = false;
		speed = -originalSpeed;
		canTurn = true;
	}

	IEnumerator TurnRight(float originalSpeed)
	{
		anim.SetBool ("isIdle", true);
		yield return new WaitForSeconds (Random.Range (minDelay, maxDelay));
		anim.SetBool ("isIdle", false);
		sr.flipX = true;
		speed = -originalSpeed;
		canTurn = true;
	}

}
