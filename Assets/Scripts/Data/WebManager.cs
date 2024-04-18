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
	public TMPro.TMP_InputField urlField;
	[SerializeField]
	private string url;

	private WebData<TempData> WebData = new WebData<TempData>();
	


	public void PressUrlButton()
	{
		url = urlField.text;
		StartCoroutine(InputUrl(url));
	}


	private IEnumerator InputUrl(string url)
	{
		this.url = url;


		StartCoroutine(WebData.Request(url));

		yield return new WaitForSeconds(0.5f);

		foreach (TempData data in WebData.DataList)
		{
			Debug.Log(data.id + " : "+ data.name);
		}
	}



}

public class WebData<T>
{
	public List<T> DataList = null;

	
	public IEnumerator Request(string url)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
		{
			// ��û ������
			yield return webRequest.SendWebRequest();

			// ��û�� �Ϸ�Ǿ����� Ȯ��
			if (webRequest.result != UnityWebRequest.Result.Success)
			{
				// ���� ó��
				Debug.LogError("Error: " + webRequest.error);
			}
			else
			{
				// ��û�� ������ ��� ���� �ޱ�
				string responseText = webRequest.downloadHandler.text;
				Debug.Log(responseText);
				DataList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(responseText);

				Debug.Log($"������ {DataList.Count}�� ���� �Ϸ�!");
				// ���⼭ ���� �����͸� ó���� �� �ֽ��ϴ�.

			}
		}
	}

}

public class TempData
{
	public int postId;
	public int id;
	public string name;
	public string email;
	public string body;
}

public class EnviromentData
{
	public string Time;
	public float Temperature;
	public float Humidity;
	public bool IsDangerous;

}
