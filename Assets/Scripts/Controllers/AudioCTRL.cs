using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the audio in the game
/// </summary>

public class AudioCTRL : MonoBehaviour
{
	public static AudioCTRL instance;
	public PlayerAudio playerAudio;
	public AudioFX audioFX;

	public Transform player;
	public GameObject BGMusic;
	public GameObject Music, Sound;
	public Sprite MusicOn, MusicOff;
	public Sprite SoundOn, SoundOff;


	[Tooltip("To toggle sound on/off from the inspector")]
	public bool soundOn;
	[Tooltip("To toggle background music on/off from the inspector")]
	public bool bgMusicOn;

	void Start () 
	{
		if (instance == null)
			instance = this;

		if (DataCTRL.instance.data.playMusic) 
		{
			BGMusic.SetActive (true);
			Music.GetComponent<Image> ().sprite = MusicOn;
		} 

		else 
		{
			BGMusic.SetActive (false);
			Music.GetComponent<Image> ().sprite = MusicOff;
		}

		if (DataCTRL.instance.data.playSound) 
		{
			soundOn = true;
			Sound.GetComponent<Image> ().sprite = SoundOn;
		} 

		else 
		{
			soundOn = false;
			Sound.GetComponent<Image> ().sprite = SoundOff;
		}
	}

	public void PlayerJump(Vector3 pos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.playerJump, pos);
		}
	}

	public void CoinPickup(Vector3 pos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.coinPickup, pos);
		}
	}

	public void FireBullets(Vector3 pos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.fireBullets, pos);
		}
	}

	public void EnemyExplosion(Vector3 pos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.enemyExplosion, pos);
		}
	}

	public void BreakCrates(Vector3 pos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.breakCrates, pos);
		}
	}

	public void WaterSplash(Vector3 pos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.waterSplash, pos);
		}
	}

	public void PowerUp(Vector3 pos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.powerUp, pos);
		}
	}

	public void KeyFound(Vector3 pos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.keyFound, pos);
		}
	}

	public void EnemyStomped(Vector3 pos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.enemyStomped, pos);
		}
	}

	public void PLayerDied(Vector3 pos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.playerDied, pos);
		}
	}

	public void ToggleSound()
	{
		if (DataCTRL.instance.data.playSound) 
		{
			soundOn = false;
			Sound.GetComponent<Image> ().sprite = SoundOff;

			DataCTRL.instance.data.playSound = false;
		}

		else
		{
			soundOn = true;
			Sound.GetComponent<Image> ().sprite = SoundOn;

			DataCTRL.instance.data.playSound = true;
		}
	}

	public void ToggleMusic()
	{
		if (DataCTRL.instance.data.playMusic) 
		{
			BGMusic.SetActive (false);
			Music.GetComponent<Image> ().sprite = MusicOff;

			DataCTRL.instance.data.playMusic = false;
		}

		else
		{
			BGMusic.SetActive (true);
			Music.GetComponent<Image> ().sprite = MusicOn;

			DataCTRL.instance.data.playMusic = true;
		}
	}

}
