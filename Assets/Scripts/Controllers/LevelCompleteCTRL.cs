using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Handles the Level Complete UI elements
/// </summary>


public class LevelCompleteCTRL : MonoBehaviour 
{
	public Button next;
	public Sprite goldenStar;
	public Image star1;
	public Image star2;
	public Image star3;
	public Text textScore;

	[HideInInspector]
	public int score;
	public int level;
	public int scoreStar1;
	public int scoreStar2;
	public int scoreStar3;
	public int scoreNextLevel;

	public float animStartDelay;
	public float animDelay;

	bool show2,show3;

	void Start () 
	{
		score = GameCTRL.instance.GetScore ();

		textScore.text = "" + score;

		if (score >= scoreStar3) 
		{
			GameCTRL.instance.SetStarsAwarded (level, 3);
			show3 = true;
			Invoke ("ShowGoldenStar", animStartDelay);
		}

		if (score >= scoreStar2 && score < scoreStar3) 
		{
			GameCTRL.instance.SetStarsAwarded (level, 2);
			show2 = true;
			Invoke ("ShowGoldenStar", animStartDelay);
		}

		if (score >= scoreStar1 && score < scoreStar2) 
		{
			GameCTRL.instance.SetStarsAwarded (level, 1);
			Invoke ("ShowGoldenStar", animStartDelay);
		}
	}

	void ShowGoldenStar()
	{
		StartCoroutine ("ShowStar1",star1);
	}

		// Generating first golden star

	IEnumerator ShowStar1(Image star)
	{
		DoAnim (star);

		yield return new WaitForSeconds (animDelay);

		if (show2 || show3)
			StartCoroutine ("ShowStar2", star2);
		else
			Invoke ("CheckLevelStatus", 1f);
	}

		// Generating second golden star

	IEnumerator ShowStar2(Image star)
	{
		DoAnim (star);

		yield return new WaitForSeconds (animDelay);

		if (show3)
			StartCoroutine ("ShowStar3", star3);
		else
			Invoke ("CheckLevelStatus", 1f);

		show2 = false;
	}

		// Generating third golden star

	IEnumerator ShowStar3(Image star)
	{
		DoAnim (star);

		yield return new WaitForSeconds (animDelay);

		Invoke ("CheckLevelStatus", 1f);

		show3 = false;
	}

		// Checking whether next level is accessible

	void CheckLevelStatus()
	{
		if (score >= scoreNextLevel) 
		{
			next.interactable = true;

			sfxCTRL.instance.ShowBulletSparkle (next.gameObject.transform.position);
			AudioCTRL.instance.KeyFound (next.gameObject.transform.position);

			GameCTRL.instance.UnlockLevel (++level);
		} 
		else
			next.interactable = false;
	}

		// Adding animation to the stars

	void DoAnim(Image star)
	{
		star.rectTransform.sizeDelta = new Vector2 (250f, 250f);

		star.sprite = goldenStar;

		star.rectTransform.DOSizeDelta (new Vector2 (200f, 200f), 0.5f, false);

		sfxCTRL.instance.ShowBulletSparkle (star.gameObject.transform.position);
		AudioCTRL.instance.KeyFound (star.gameObject.transform.position);
	}

}
