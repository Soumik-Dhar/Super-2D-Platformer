using System;
using UnityEngine;

[Serializable]

/// <summary>
/// Creates the special effects game objects
/// </summary>

public class SFX
{
	public GameObject sfx_coin_pickup;    // shown when player picks up coins
	public GameObject sfx_bullet_pickup;    // shown when player picks up the bullet key
	public GameObject sfx_player_lands;    // shown when the player lands after jumping 
	public GameObject sfx_fragment1;	// shown when crate breaks into fragments
	public GameObject sfx_fragment2;	// shown when crate breaks into fragments
	public GameObject sfx_splash;	// shown when player falls into water
	public GameObject sfx_enemy_explosion;	// shown when bullets hit the enemy
}
