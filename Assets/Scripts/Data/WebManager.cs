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
			// 요청 보내기
			yield return webRequest.SendWebRequest();

			// 요청이 완료되었는지 확인
			if (webRequest.result != UnityWebRequest.Result.Success)
			{
				// 에러 처리
				Debug.LogError("Error: " + webRequest.error);
			}
			else
			{
				// 요청이 성공한 경우 응답 받기
				string responseText = webRequest.downloadHandler.text;
				Debug.Log(responseText);
				DataList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(responseText);

				Debug.Log($"데이터 {DataList.Count}개 수신 완료!");
				// 여기서 응답 데이터를 처리할 수 있습니다.

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
