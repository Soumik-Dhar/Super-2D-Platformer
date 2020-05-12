using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

/// <summary>
/// Handles the ads in the game
/// </summary>

public class AdsCTRL : MonoBehaviour 
{
	public static AdsCTRL instance = null;
	public string Android_AdMob_Banner_ID;
	public string Android_AdMob_Interstitial_ID;

	public bool testMode;
	public bool showBanner;
	public bool showInterstitial;

	BannerView bannerView;
	InterstitialAd interstitial;

	void Start () 
	{
		if (instance == null) 
		{
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else
			Destroy (gameObject);
	}

		// Banner Ads

	public void RequestBanner()
	{
		if(testMode)
			bannerView = new BannerView (Android_AdMob_Banner_ID, AdSize.Banner, AdPosition.Bottom);
		else
		{
			// Code for live ad
		}

		AdRequest adRequest = new AdRequest.Builder ().Build ();

		bannerView.LoadAd (adRequest);

		HideBanner ();
	}

	public void ShowBanner()
	{
		if(showBanner)
			bannerView.Show ();
	}

	public void HideBanner()
	{
		if(showBanner)
			bannerView.Hide ();
	}

	public void HideBanner(float duration)
	{
		if(showBanner)
			StartCoroutine ("HideBannerRoutine", duration);
	}

	IEnumerator HideBannerRoutine(float duration)
	{
		yield return new WaitForSeconds (duration);
		bannerView.Hide ();
	}

		// Interstitial Ads

	public void RequestInterstitial()
	{
		if (testMode)
			interstitial = new InterstitialAd (Android_AdMob_Interstitial_ID);
		else
		{
			// Code for live ad
		}

		AdRequest adRequest = new AdRequest.Builder ().Build ();

		interstitial.LoadAd (adRequest);

		interstitial.OnAdClosed += HandleOnAdClosed;
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		interstitial.Destroy ();
		RequestInterstitial ();
	}

	public void ShowInterstitial()
	{
		if (showInterstitial) 
		{
			if (interstitial.IsLoaded()) 
			{
				interstitial.Show ();
			}
		}
	}

	void OnEnable()
	{
		if (showBanner) 
			RequestBanner ();
		
		if (showInterstitial) 
			RequestInterstitial ();
	}

	void OnDisable()
	{
		if (showBanner) 
			bannerView.Destroy ();
		
		if (showInterstitial) 
			interstitial.Destroy ();
	}

}
