using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DG.Tweening;

/// <summary>
/// Manages game mechanics like lives count, reload screens, score updates, HUD controls etc
/// </summary>

public class GameCTRL : MonoBehaviour 
{
	public static GameCTRL instance;
	public float restartDelay;
	public float maxTime;
	public bool timerOn;
	float timeLeft;
	bool isPaused;

	static int countEnemy=1;

	public Transform player;
	public GameObject lever;
	public GameObject ShiningCoin;
	public GameObject EnemySpawner;
	public GameObject SignedPlatform;

	[HideInInspector]
	public GameData data;
	public UI ui;

	Button Pause;

	public enum Item
	{
		Coin,
		ShiningCoin,
		Enemy
	}

	void Awake()
	{
		if (instance == null) 
			instance = this;
	}

	void Start () 
	{
		DataCTRL.instance.RefreshData ();
		data = DataCTRL.instance.data;

		if (data.screenRefresh == true) 
		{
			data.screenRefresh = false;
			ReLoadLevel ();
		}

		RefreshUI ();

		timeLeft = maxTime;
		HandleFirstBoot ();
		UpdateLives ();

		timerOn = true;
		isPaused = false;

		RemoveCollectedCoins ();
		RemoveCollectedKeys ();
		RemoveBrokenCrates ();
		RemoveKilledEnemies ();
		RemoveBulletPowerUp ();
	}
	
	void Update () 
	{
		if (isPaused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;

		if (timeLeft > 0 && timerOn)
			UpdateTimer ();

		ManageKeyStates ();
	}

	public void RefreshUI () 
	{
		ui.textCoinCount.text = "x" + data.coinCount;
		ui.textScore.text = "score :" + data.score;
	}

	public void ResetData()
	{
		data.coinCount = 0;
		ui.textCoinCount.text = "x" + data.coinCount;

		data.score = 0;
		ui.textScore.text = "score :" + data.score;

		data.lives = 3;

		for (int keyNumber = 0; keyNumber < 3; keyNumber++)
			data.keyFound [keyNumber] = false;
	}
		
	void OnEnable()
	{
		Debug.Log ("Data Loaded");
		RefreshUI ();
	}

	void OnDisable()
	{
		Debug.Log ("Data Saved");
		DataCTRL.instance.SaveData (data);

		Time.timeScale = 1;

		AdsCTRL.instance.HideBanner ();
	}
		
	public void UpdateCoinCount()
	{
		data.coinCount += 1;
		ui.textCoinCount.text = "x" + data.coinCount;
	}

	public void UpdateScore(Item item)
	{
		int itemValue = 0;

		switch (item) 
		{
		case Item.Coin:
			itemValue = 5;
			break;
		case Item.ShiningCoin:
			itemValue = 10;
			break;
		case Item.Enemy:
			itemValue = 20;
			break;
		default:
			break;
		}
			
		data.score += itemValue;
		ui.textScore.text = "score :" + data.score;
	}

	public void UpdateKeyCount(int keyNumber)
	{
		data.keyFound [keyNumber] = true;

		if (keyNumber == 0)
			ui.key0.sprite = ui.key0full;
		else if (keyNumber == 1)
			ui.key1.sprite = ui.key1full;
		else if (keyNumber == 2)
			ui.key2.sprite = ui.key2full;
	}

	void ManageKeyStates()
	{
		if (data.keyFound [0])
			ui.key0.sprite = ui.key0full;
		if (data.keyFound [1])
			ui.key1.sprite = ui.key1full;
		if (data.keyFound [0] && data.keyFound [1]) 
			ShowSignedPlatform ();
	}

	public void UpdateTimer()
	{
		timeLeft -= Time.deltaTime;
		ui.textTimer.text = "Timer :" + (int)timeLeft;

		if (timeLeft <= 0) 
		{
			ui.textTimer.text = "Timer : 0";
			GameObject player = GameObject.FindGameObjectWithTag ("Player") as GameObject;
			PlayerDied (player);
		}
	}

	public int GetScore()
	{
		return data.score;
	}

	public void SetStarsAwarded(int level, int stars)
	{
		data.levelData [level].stars = stars;
	}

	public void UnlockLevel(int level)
	{
		data.levelData [level].isUnlocked = true;
	}

	void ShowSignedPlatform ()
	{
		SignedPlatform.SetActive (true);

		sfxCTRL.instance.ShowPlayerLanding (SignedPlatform.transform.position);
	}

	public void LevelComplete()
	{
		data.lives = 3;

		ui.panelMobileUI.SetActive (false);
		ui.LevelCompleteMenu.SetActive (true);
		PlayerCTRL.instance.freeze ();
	}

	public void BulletHitEnemy(Transform enemy)
	{
		Vector3 pos = enemy.position;
		pos.y = enemy.transform.position.y + 1f;
		pos.z = 20f;

		sfxCTRL.instance.ShowEnemyExplosion (pos);
		AudioCTRL.instance.EnemyExplosion (enemy.position);

		Destroy (enemy.gameObject);

		Instantiate (ShiningCoin, pos, Quaternion.identity);

		UpdateScore (Item.Enemy);

		if(!(enemy.CompareTag("LevelOneBoss") || enemy.CompareTag("Monster")))
		{
			PlayerPrefs.SetString ("Enemy" + countEnemy, enemy.parent.name);
			PlayerPrefs.SetInt ("CountEnemies",countEnemy++);	
		}
	}

	public void PlayerStompsEnemy(GameObject enemy)
	{
		enemy.tag = "Untagged";

		Vector3 pos = enemy.transform.position;
		pos.y = enemy.transform.position.y + 1f;
		pos.z = 20f;

		AudioCTRL.instance.EnemyStomped (enemy.transform.position);

		Destroy (enemy);

		Instantiate (ShiningCoin, pos, Quaternion.identity);

		UpdateScore (Item.Enemy);

		PlayerPrefs.SetString ("Enemy" + countEnemy, enemy.transform.parent.name);
		PlayerPrefs.SetInt ("CountEnemies",countEnemy++);
	}

	public void PlayerDied(GameObject player)
	{
		player.SetActive (false);

		StopCameraFollow ();

		CheckLives ();
	}

	public void PlayerDrowned(GameObject player)
	{
		StopCameraFollow ();

		CheckLives ();
	}

	public void PlayerDeathAnimation(GameObject player)
	{
			// Throwing player back
		Rigidbody2D rb = player.GetComponent<Rigidbody2D> ();
		rb.AddForce (new Vector2 (-200f, 400f));

			// Rotating player
		player.transform.Rotate (0, 0, 45f);

			// Disabling player controller script
		player.GetComponent<PlayerCTRL> ().enabled = false;

			// Disabling collider of player
		foreach (Collider2D col in player.transform.GetComponents<Collider2D>())
		{
			col.enabled = false;	
		}

			// Disabling child objects of player 
		foreach (Transform child in player.transform) 
		{
			child.gameObject.SetActive (false);
		}

			// Disabling camera controller script
		Camera.main.GetComponent<CameraCTRL> ().enabled = false;

			// Freezing player
		rb.velocity = Vector2.zero;

			// Waiting for restart
		StartCoroutine("PauseBeforeRestart",player);
	}

	IEnumerator PauseBeforeRestart(GameObject player)
	{
		yield return new WaitForSeconds (1.5f);

		PlayerDied (player);
	}

	void CheckLives()
	{
		int updatedLives = data.lives;
		updatedLives -= 1;
		data.lives = updatedLives;

		if (data.lives == 0) 
		{
			ui.life1.sprite = ui.zeroLife;
			ui.life2.sprite = ui.zeroLife;
			ui.life3.sprite = ui.zeroLife;

			ResetData();

			DataCTRL.instance.SaveData (data);

			Invoke ("GameOver", restartDelay);
		}
		else 
		{
			DataCTRL.instance.SaveData (data);
			Invoke ("RestartLevel", restartDelay);
		}
	}

	void UpdateLives()
	{
		if (data.lives == 3) 
		{
			ui.life1.sprite = ui.fullLife;
			ui.life2.sprite = ui.fullLife;
			ui.life3.sprite = ui.fullLife;
		}

		if (data.lives == 2)
		{
			ui.life1.sprite = ui.zeroLife;
		}

		if (data.lives == 1) 
		{
			ui.life1.sprite = ui.zeroLife;
			ui.life2.sprite = ui.zeroLife;
		}
			
	}

	void HandleFirstBoot ()
	{
		if (data.isFirstBoot) 
		{
			data.lives = 3;
			data.coinCount = 0;
			data.score = 0;
			for (int keyNumber = 0; keyNumber < 3; keyNumber++)
				data.keyFound [keyNumber] = false;
			data.isFirstBoot = false;
		}
	}

	public void ShowLever()
	{
		lever.SetActive (true);

		sfxCTRL.instance.ShowPlayerLanding (lever.gameObject.transform.position);
		AudioCTRL.instance.KeyFound (lever.gameObject.transform.position);
	}

	public void StopCameraFollow ()
	{
		Camera.main.GetComponent<CameraCTRL> ().enabled = false;

		player.GetComponent<PlayerCTRL> ().isStuck = true;
		player.transform.Find ("LeftCheck").gameObject.SetActive(false);
		player.transform.Find ("RightCheck").gameObject.SetActive(false);
	}

	public void ActivateEnemySpawner()
	{
		EnemySpawner.SetActive (true);
	}

	public void DeactivateEnemySpawner()
	{
		EnemySpawner.SetActive (false);
	}

	void SetPause()
	{
		isPaused = true;
	}

	public void ShowPausePanel()
	{
		if (ui.panelMobileUI.activeInHierarchy)
			ui.panelMobileUI.SetActive (false);
		if (ui.panelHUD.activeInHierarchy)
			ui.panelHUD.SetActive (false);

		ui.panelPause.SetActive (true);
		ui.panelPause.gameObject.GetComponent<RectTransform> ().DOAnchorPosY (0, 0.7f, false).OnComplete (SetPause);

		AdsCTRL.instance.ShowBanner ();

		AdsCTRL.instance.ShowInterstitial ();
	}

	public void HidePausePanel()
	{
		isPaused = false;

		if (!ui.panelMobileUI.activeInHierarchy)
			ui.panelMobileUI.SetActive (true);
		if (!ui.panelHUD.activeInHierarchy)
			ui.panelHUD.SetActive (true);

		ui.panelPause.gameObject.GetComponent<RectTransform> ().DOAnchorPosY (1080, 0.7f, false);

		AdsCTRL.instance.HideBanner ();
	}

		// Removing collected coins

	public void RemoveCollectedCoins()
	{
		int i = PlayerPrefs.GetInt ("CountCoins");
		while (i > 0) 
		{
			Destroy(GameObject.Find(PlayerPrefs.GetString ("Coin" + i--)));
		}
	}

		// Removing collected keys

	public void RemoveCollectedKeys()
	{
		int i = PlayerPrefs.GetInt ("CountKeys");
		while (i > 0) 
		{
			Destroy(GameObject.Find(PlayerPrefs.GetString ("Key" + i--)));
		}
	}

	// Removing broken crates

	public void RemoveBrokenCrates()
	{
		int i = PlayerPrefs.GetInt ("CountCrates");
		while (i > 0) 
		{
			Destroy(GameObject.Find(PlayerPrefs.GetString ("Crate" + i--)));
		}
	}

	// Removing killed enemies

	public void RemoveKilledEnemies()
	{
		int i = PlayerPrefs.GetInt ("CountEnemies");
		while (i > 0) 
		{
			Destroy(GameObject.Find(PlayerPrefs.GetString ("Enemy" + i--)));
		}
	}

	// Removing bullet powerup

	public void RemoveBulletPowerUp()
	{
		if (PlayerPrefs.GetInt ("BPU") == 1)
			Destroy (GameObject.FindGameObjectWithTag ("BulletPowerUp"));
	}

	void GameOver()
	{
		if (ui.panelMobileUI.activeInHierarchy)
			ui.panelMobileUI.SetActive (false);
		if (ui.panelHUD.activeInHierarchy)
			ui.panelHUD.SetActive (false);
		
		ui.panelGameOver.SetActive (true);
		ui.panelGameOver.gameObject.GetComponent<RectTransform> ().DOAnchorPosY (0, 0.7f, false);

		AdsCTRL.instance.ShowBanner ();
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void ReLoadLevel()
	{
		ResetData ();
		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

}
