using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUICTRL : MonoBehaviour 
{
	public GameObject player;
	PlayerCTRL playerctrl;

	void Start () 
	{
		playerctrl = player.GetComponent<PlayerCTRL> ();
	}
		
	public void MobileMoveLeft()
	{
		playerctrl.MobileMoveLeft ();
	}

	public void MobileMoveRight()
	{
		playerctrl.MobileMoveRight ();
	}

	public void MobileJump()
	{
		playerctrl.MobileJump ();
	}

	public void MobileStop()
	{
		playerctrl.MobileStop ();
	}

	public void MobileFireBullets()
	{
		playerctrl.MobileFireBullets ();
	}

}
