using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the platform between two positions
/// </summary>

public class MovingPlatformCTRL : MonoBehaviour 
{
	public Transform pos1,pos2,startPos;
	public float speed;

	Vector3 nextPos;

	void Start () 
	{
		nextPos = startPos.position;		
	}
	
	void Update () 
	{
		if (transform.position == pos1.position)
			nextPos = pos2.position;
			
		if (transform.position == pos2.position)
			nextPos = pos1.position;
		
		transform.position = Vector3.MoveTowards (transform.position, nextPos, speed * Time.deltaTime);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawLine (pos1.position, pos2.position);
	}

}
