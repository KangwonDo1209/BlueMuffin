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
	private string EnviromentDataUrl;
	private string SensorDataUrl;
	public string EnviromentDataPath;
	public string SensorDataPath;

	private WebData<TempData> WebEnviromentData = new WebData<TempData>();
	private WebData<RoomSensorData> WebRoomSensorData = new WebData<RoomSensorData>();
	public override void Awake()
	{
		base.Awake();

	}

	public void RequestToFieldUrl()
	{
		url = urlField.text;	// 입력 URL

		// 세부 경로 설정
		EnviromentDataUrl = url + EnviromentDataUrl;
		SensorDataUrl = url + SensorDataUrl;

		// 데이터 요청
		StartCoroutine(WebEnviromentData.Request(EnviromentDataUrl));
		StartCoroutine(WebRoomSensorData.Request(SensorDataUrl));
	}

	public void DebugAllData() // 데이터 출력 디버깅용 메소드
	{
		foreach (TempData data in WebEnviromentData.DataList)
		{
			Debug.Log(data.id + " : " + data.name);
		}
	}
}
