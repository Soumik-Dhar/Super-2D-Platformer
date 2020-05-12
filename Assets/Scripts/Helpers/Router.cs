using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Calls methods from static classes
/// </summary>

public class Router : MonoBehaviour 
{
	public void ShowPausePanel()
	{
		GameCTRL.instance.ShowPausePanel ();
	}

	public void HidePausePanel()
	{
		GameCTRL.instance.HidePausePanel ();
	}

	public void ToggleSound()
	{
		AudioCTRL.instance.ToggleSound ();
	}

	public void ToggleMusic()
	{
		AudioCTRL.instance.ToggleMusic ();
	}

}
