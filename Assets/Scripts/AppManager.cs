using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Chapter.Singleton;

public class AppManager : Singleton<AppManager>
{
	public string urlKey = "URL";
	public override void Awake()
	{
		base.Awake();
	}
	public void SaveUrl(string url)
	{
		PlayerPrefs.SetString(urlKey, url);
		PlayerPrefs.Save();
	}
	public string LoadUrl()
	{
		return PlayerPrefs.GetString(urlKey, "NULL");
	}

	public void QuitApplication()
	{
		Application.Quit();
	}
	public void SceneReset()
	{
		SceneManager.LoadScene(0);
	}
}
