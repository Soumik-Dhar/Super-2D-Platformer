using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides functionality to the social buttons
/// </summary>

public class SettingsCTRL : MonoBehaviour 
{
	public string facebookURL, twitterURl, ratingsURL;

	public void Facebook()
	{
		Application.OpenURL (facebookURL);
	}

	public void Twitter()
	{
		Application.OpenURL (twitterURl);
	}

	public void Ratings()
	{
		Application.OpenURL (ratingsURL);
	}

}
