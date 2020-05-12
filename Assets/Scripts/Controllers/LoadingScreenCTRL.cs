using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenCTRL : MonoBehaviour 
{
	public GameObject panelLoading;
	public static LoadingScreenCTRL instance;

	void Start () 
	{
		if (instance == null)
			instance = this;
	}

	public void ShowLoading()
	{
		panelLoading.SetActive (true);
	}

}
