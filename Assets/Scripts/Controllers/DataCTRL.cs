using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Provides centralized database access by using a singleton class
/// for creating a persistent dataCTRL object across multiple scenes
/// </summary>

public class DataCTRL : MonoBehaviour 
{
	public static DataCTRL instance = null;

	public GameData data;

	public bool devMode;
	string dataFilePath;

	BinaryFormatter bf;

	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
			DontDestroyOnLoad (gameObject);
		} 
		else
			Destroy (gameObject);

		bf=new BinaryFormatter();

		dataFilePath = Application.persistentDataPath + "/game.dat";

//		Debug.Log (dataFilePath);
	}

	public void RefreshData()
	{
		if (File.Exists (dataFilePath)) 
		{
			FileStream fs = new FileStream (dataFilePath, FileMode.Open);
			data = (GameData)bf.Deserialize (fs);
			fs.Close ();

//			Debug.Log ("Data Refreshed");
		}
	}

	public void SaveData()
	{
		FileStream fs = new FileStream (dataFilePath, FileMode.Create);
		bf.Serialize (fs, data);
		fs.Close ();

//		Debug.Log ("Data Saved");
	}

	public void SaveData(GameData data)
	{
		FileStream fs = new FileStream (dataFilePath, FileMode.Create);
		bf.Serialize (fs, data);
		fs.Close ();

//		Debug.Log ("Data Saved");
	}

	public bool isUnlocked(int level)
	{
		return data.levelData [level].isUnlocked;
	}

	public int getStars(int level)
	{
		return data.levelData [level].stars;
	}

	void OnEnable()
	{
		CheckDB ();
	}

	void CheckDB()
	{
		if (!File.Exists (dataFilePath)) 
		{
			#if UNITY_ANDROID
			CopyDB ();
			#endif
		} 
		else 
		{
			if(SystemInfo.deviceType==DeviceType.Desktop)
			{
				string destFile = Path.Combine (Application.streamingAssetsPath, "game.dat");
				File.Delete (destFile);
				File.Copy (dataFilePath, destFile);
			}

			if (devMode) 
			{
				if(SystemInfo.deviceType==DeviceType.Handheld)
				{
					File.Delete (dataFilePath);
					CopyDB ();
				}
			}

			RefreshData ();
		}
	}

	void CopyDB()
	{
		string srcFile = Path.Combine (Application.streamingAssetsPath, "game.dat");
		WWW downloader = new WWW (srcFile);

		while (!downloader.isDone) 
		{}

		File.WriteAllBytes (dataFilePath, downloader.bytes);

		RefreshData ();
	}

	public void RefreshScreen()
	{
		data.screenRefresh = true;
		SaveData ();
		PlayerPrefs.DeleteAll ();
	}

}
