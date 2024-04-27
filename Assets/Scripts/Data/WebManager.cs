using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Net.Http;
using Unity.VisualScripting;
using UnityEngine.UI;
using JetBrains.Annotations;
using TMPro;


public class WebManager : MonoBehaviour
{
	public TMP_InputField urlField;
	[SerializeField]
	private string url;

	private WebData<TempData> WebData = new WebData<TempData>();

	public void RequestToFieldUrl() 
	{
		url = urlField.text;
		StartCoroutine(WebData.Request(url));
	}

	public void DebugData() // ������ ��� ������ �޼ҵ�
	{
		foreach (TempData data in WebData.DataList)
		{
			Debug.Log(data.id + " : " + data.name);
		}
	}
}

public class WebData<T>
{
	// ������ ����Ʈ
	public List<T> DataList = new List<T>();

	// ������ ��û
	public IEnumerator Request(string url)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
		{
			// ��û ������
			yield return webRequest.SendWebRequest();

			// ��û�� �Ϸ�Ǿ����� Ȯ��
			if (webRequest.result != UnityWebRequest.Result.Success)
			{
				// ���� ���
				Debug.LogError("Error: " + webRequest.error);
			}
			else
			{
				// ��û ������ ������ȭ�Ͽ� ����Ʈ�� ����
				string responseText = webRequest.downloadHandler.text;
				Debug.Log(responseText);
				DataList = JsonConvert.DeserializeObject<List<T>>(responseText);

				// ������ ���� Ȯ��
				Debug.Log($"������ {DataList.Count}�� ���� �Ϸ�!");
				
			}
		}
	}

}

public class TempData // �׽�Ʈ�� Ŭ����
{
	public int postId;
	public int id;
	public string name;
	public string email;
	public string body;
}

public class EnviromentData // (�̿�) ������ Ŭ����
{
	public string Time;
	public float Temperature;
	public float Humidity;
	public bool IsDangerous;
}
