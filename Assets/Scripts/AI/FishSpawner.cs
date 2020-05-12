using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns jumping fish every n seconds
/// </summary>

public class FishSpawner : MonoBehaviour 
{
	public GameObject fish;

	public float spawnDelay;
	public bool canSpawn;

	void Start () 
	{
		canSpawn = true;
	}

	void Update () 
	{
		if(canSpawn)
			StartCoroutine ("SpawnFish");
	}

	IEnumerator SpawnFish()
	{
		Instantiate (fish, transform.position, Quaternion.identity);
		canSpawn = false;
		yield return new WaitForSeconds (spawnDelay);
		canSpawn = true;
	}

}
