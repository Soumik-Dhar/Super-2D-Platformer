using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

/// <summary>
/// Data model for game data
/// </summary>

public class GameData
{
	public int coinCount;
	public int lives;
	public int score;
	public bool[] keyFound;
	public LevelData[] levelData;
	public bool isFirstBoot;
	public bool playSound;
	public bool playMusic;
	public bool screenRefresh;
}
