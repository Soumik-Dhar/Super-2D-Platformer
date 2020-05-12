using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Locks/Unlocks a level button and shows number of stars awarded to each unlocked level
/// </summary>

public class ButtonCTRL : MonoBehaviour
{
	int level;
	Button button;
	Image image;
	Text text;
	Transform star1, star2, star3;

	public string sceneName;

	public Sprite locked;
	public Sprite unLocked;

	void Start () 
	{
		level = int.Parse (transform.gameObject.name);

		button = transform.gameObject.GetComponent<Button> ();
		image = button.GetComponent<Image> ();
		text = button.gameObject.transform.GetChild (0).GetComponent<Text> ();

		star1 = button.gameObject.transform.GetChild (1);
		star2 = button.gameObject.transform.GetChild (2);
		star3 = button.gameObject.transform.GetChild (3);

		ButtonStatus ();
			
	}

	void ButtonStatus () 
	{
		bool unLocked = DataCTRL.instance.isUnlocked (level);
		int stars = DataCTRL.instance.getStars (level);


		if (unLocked) 
		{
			if (stars == 3) 
			{
				star1.gameObject.SetActive (true);
				star2.gameObject.SetActive (true);
				star3.gameObject.SetActive (true);
			}

			if (stars == 2) 
			{
				star1.gameObject.SetActive (true);
				star2.gameObject.SetActive (true);
				star3.gameObject.SetActive (false);
			}

			if (stars == 1) 
			{
				star1.gameObject.SetActive (true);
				star2.gameObject.SetActive (false);
				star3.gameObject.SetActive (false);
			}

			if (stars == 0) 
			{
				star1.gameObject.SetActive (false);
				star2.gameObject.SetActive (false);
				star3.gameObject.SetActive (false);
			}
				
			button.onClick.AddListener (LoadScene);
		}

		else 
		{
			image.overrideSprite = locked;
			text.text = " ";

			star1.gameObject.SetActive (false);
			star2.gameObject.SetActive (false);
			star3.gameObject.SetActive (false);
		}
	}

	public void LoadScene()
	{
		DataCTRL.instance.RefreshScreen ();

		LoadingScreenCTRL.instance.ShowLoading ();
		SceneManager.LoadScene (sceneName);
	}

}
