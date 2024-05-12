using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using Unity.VisualScripting;
using UnityEngine.UI;
using JetBrains.Annotations;
using TMPro;
using Chapter.Singleton;
using System;
// using System.Data.SqlClient;

public class WebManager : Chapter.Singleton.Singleton<WebManager>
{
	public int periodSec;
	Coroutine GenerateCouroutine;

	public TMP_InputField urlField;
	[SerializeField]
	private string url;
	private string EnviromentDataUrl;
	private string SensorDataUrl;
	public string EnviromentDataPath;
	public string SensorDataPath;

	public WebData<EnvironmentData> WebEnviromentData = new WebData<EnvironmentData>();
	public WebData<RoomSensorData> WebRoomSensorData = new WebData<RoomSensorData>();
	public override void Awake()
	{
		base.Awake();

		LoadURL();
	}

	public void LoadURL()
	{
		StopGenerateCouroutine();
		string loadUrl = AppManager.Instance.LoadUrl();
		Debug.Log(loadUrl);
		if (loadUrl == "NULL")
			return;
		url = loadUrl;
		StartLoadDataPeriodically(periodSec);
	}

	public void RequestToFieldUrl()
	{
		url = urlField.text;    // 입력 URL
		AppManager.Instance.SaveUrl(url);

		// 세부 경로 설정
		EnviromentDataUrl = url + EnviromentDataUrl;
		SensorDataUrl = url + SensorDataUrl;

		LoadURL();
		// 데이터 요청
		//StartCoroutine(WebEnviromentData.Request(EnviromentDataUrl));
		//StartCoroutine(WebRoomSensorData.Request(SensorDataUrl));
	}

	public void DebugAllData() // 데이터 출력 디버깅용 메소드
	{
		foreach (EnvironmentData data in WebEnviromentData.DataList)
		{
			Debug.Log(CombineDataString(data));
		}
	}
	public string CombineDataString(EnvironmentData data)
	{
		string str = "";
		string s = " / ";
		str += data.Time + s;
		str += data.RoomId + s;
		str += data.Temperature + s;
		str += data.Humidity + s;
		str += data.Gas + s;
		str += data.Dust + s;
		str += data.DangerCode + s;
		return str;
	}
	public void StartLoadDataPeriodically(int period)
	{
		StopGenerateCouroutine();
		GenerateCouroutine = StartCoroutine(LoadData(period));
	}
	public IEnumerator LoadData(int period)
	{
		while (true)
		{
			StartCoroutine(WebEnviromentData.Request(EnviromentDataUrl));
			StartCoroutine(WebRoomSensorData.Request(SensorDataUrl));
			Debug.Log("Load!");
			yield return new WaitForSeconds(period);
		}
	}

	public void StopGenerateCouroutine()
	{
		if (GenerateCouroutine != null)
			StopCoroutine(GenerateCouroutine);
	}

}
