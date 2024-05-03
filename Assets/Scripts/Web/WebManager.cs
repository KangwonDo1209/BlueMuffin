using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using Unity.VisualScripting;
using UnityEngine.UI;
using JetBrains.Annotations;
using TMPro;
using Chapter.Singleton;
// using System.Data.SqlClient;

public class WebManager : Chapter.Singleton.Singleton<WebManager>
{
	public TMP_InputField urlField;
	[SerializeField]
	private string url;

	private WebData<TempData> WebData = new WebData<TempData>();
	public override void Awake()
	{
		base.Awake();
		
	}

	public void RequestToFieldUrl() 
	{
		url = urlField.text;
		StartCoroutine(WebData.Request(url));
	}

	public void DebugAllData() // 데이터 출력 디버깅용 메소드
	{
		foreach (TempData data in WebData.DataList)
		{
			Debug.Log(data.id + " : " + data.name);
		}
	}
}
