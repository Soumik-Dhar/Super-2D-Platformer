﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour 
{
	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}

	public void LoadCurrentScene()
	{
//		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		GameCTRL.instance.ReLoadLevel();
	}

}
