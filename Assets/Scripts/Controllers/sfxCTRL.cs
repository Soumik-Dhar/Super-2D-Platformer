using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SFX controller for particle and other effects
/// </summary>

public class sfxCTRL : MonoBehaviour 
{
	public static sfxCTRL instance;
	public SFX sfx;

	public Transform key0, key1, key2;

	void Awake()
	{
		if (instance == null)
			instance = this;
	}

	/// <summary>
	/// Shows the coin sparkle effect
	/// </summary>

	public void ShowCoinSparkle(Vector3 pos)
	{
		Instantiate (sfx.sfx_coin_pickup, pos, Quaternion.identity);
	}

	/// <summary>
	/// Shows the key sparkle effect
	/// </summary>

	public void ShowKeySparkle(int keyNumber)
	{
		Vector3 pos=Vector3.zero;

		if (keyNumber == 0)
			pos = key0.position;
		else if (keyNumber == 1)
			pos = key1.position;
		else if (keyNumber == 2)
			pos = key2.position;

		Instantiate (sfx.sfx_bullet_pickup, pos, Quaternion.identity);
	}

	/// <summary>
	/// Shows the bullet key sparkle effect
	/// </summary>

	public void ShowBulletSparkle(Vector3 pos)
	{
		Instantiate (sfx.sfx_bullet_pickup, pos, Quaternion.identity);
	}

	/// <summary>
	/// Shows the enemy explosion effect
	/// </summary>

	public void ShowEnemyExplosion(Vector3 pos)
	{
		Instantiate (sfx.sfx_enemy_explosion, pos, Quaternion.identity);
	}

	/// <summary>
	/// Shows the player landing effect
	/// </summary>

	public void ShowPlayerLanding(Vector3 pos)
	{
		Instantiate (sfx.sfx_player_lands, pos, Quaternion.identity);
	}

	/// <summary>
	/// Shows the splash effect when player falls in water
	/// </summary>

	public void ShowSplash(Vector3 pos)
	{
		Instantiate (sfx.sfx_splash, pos, Quaternion.identity);
	}

	/// <summary>
	/// Shows the crate breaking effect
	/// </summary>

	public void ShowCrateBreaking(Vector3 pos)
	{
		Vector3 pos1 = pos;
		pos1.x += 0.3f;
		Vector3 pos2 = pos;
		pos2.x -= 0.3f;
		Vector3 pos3 = pos;
		pos3.x -= 0.6f;
		pos3.y -= 0.6f;
		Vector3 pos4 = pos;
		pos4.x += 0.6f;
		pos4.y += 0.6f;


		Instantiate (sfx.sfx_fragment1, pos1, Quaternion.identity);
		Instantiate (sfx.sfx_fragment2, pos2, Quaternion.identity);
		Instantiate (sfx.sfx_fragment2, pos3, Quaternion.identity);
		Instantiate (sfx.sfx_fragment1, pos4, Quaternion.identity);

		AudioCTRL.instance.EnemyExplosion (pos);
	}

}
