using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Button save CTR.
/// </summary>

public class ButtonSaveCTRL : MonoBehaviour 
{
	public void SaveData()
	{
		DataCTRL.instance.SaveData ();
	}

}
