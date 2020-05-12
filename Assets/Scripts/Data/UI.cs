using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Groups UI elements together 
/// </summary>

[Serializable]

public class UI
{
	[Header("Text")]
	public Text textCoinCount;
	public Text textScore;
	public Text textTimer;

	[Header("Images/Sprites")]
	public Image key0;
	public Image key1;
	public Image key2;
	public Sprite key0full;
	public Sprite key1full;	
	public Sprite key2full;
	public Image life1;
	public Image life2;
	public Image life3;
	public Sprite zeroLife;
	public Sprite fullLife;

	[Header("PopUpMenus/Panels")]
	public GameObject panelMobileUI;
	public GameObject panelGameOver;
	public GameObject LevelCompleteMenu;
	public GameObject panelPause;
	public GameObject panelHUD;
}
