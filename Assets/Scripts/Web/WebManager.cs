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
		url = urlField.text;	// �Է� URL

		// ���� ��� ����
		EnviromentDataUrl = url + EnviromentDataUrl;
		SensorDataUrl = url + SensorDataUrl;

		// ������ ��û
		StartCoroutine(WebEnviromentData.Request(EnviromentDataUrl));
		StartCoroutine(WebRoomSensorData.Request(SensorDataUrl));
	}

	public void DebugAllData() // ������ ��� ������ �޼ҵ�
	{
		foreach (TempData data in WebEnviromentData.DataList)
		{
			Debug.Log(data.id + " : " + data.name);
		}
	}
}
