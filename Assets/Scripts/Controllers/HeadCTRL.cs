using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks for collision with breakable game objects and invokes corresponding sfx
/// </summary>

public class HeadCTRL : MonoBehaviour 
{
	static int countCrate=1;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Breakable")) 
		{
			sfxCTRL.instance.ShowCrateBreaking (other.gameObject.transform.parent.transform.position);

			gameObject.transform.parent.transform.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;

			PlayerPrefs.SetString ("Crate" + countCrate, other.gameObject.transform.parent.name);
			PlayerPrefs.SetInt ("CountCrates",countCrate++);

			Destroy (other.gameObject.transform.parent.gameObject);
		}
	}
}
