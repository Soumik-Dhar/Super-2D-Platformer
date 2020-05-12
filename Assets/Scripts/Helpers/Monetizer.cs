using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Show/Hide ads in the game
/// </summary>

public class Monetizer : MonoBehaviour 
{
	public bool timedAd;
	public float adDuration;

	void Start () 
	{
		AdsCTRL.instance.ShowBanner ();
	}

	void OnDisable () 
	{
		if (!timedAd)
			AdsCTRL.instance.HideBanner ();
		else
			AdsCTRL.instance.HideBanner (adDuration);
	}
}
