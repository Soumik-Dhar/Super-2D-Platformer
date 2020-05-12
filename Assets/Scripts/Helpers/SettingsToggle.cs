using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Toggles the social buttons
/// </summary>

public class SettingsToggle : MonoBehaviour 
{
	public RectTransform Facebook, Twitter, Ratings;
	public float moveFB, moveTW, moveRS;
	public float defaultX, defaultY;
	public float speed;

	bool expanded;

	void Start () 
	{
		expanded = false;
	}
	
	public void Toggle()
	{
		if (!expanded) 
		{
			Facebook.DOAnchorPosY (moveFB, speed, false);
			Twitter.DOAnchorPosY (moveTW, speed, false);
			Ratings.DOAnchorPosY (moveRS, speed, false);
			expanded = true;
		} 

		else 
		{
			Facebook.DOAnchorPosY (defaultY, speed, false);
			Twitter.DOAnchorPosY (defaultY, speed, false);
			Ratings.DOAnchorPosY (defaultY, speed, false);
			expanded = false;
		}
	}
}
