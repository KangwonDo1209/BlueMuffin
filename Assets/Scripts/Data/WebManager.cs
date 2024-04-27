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

	public void DebugData() // 데이터 출력 디버깅용 메소드
	{
		foreach (TempData data in WebData.DataList)
		{
			Debug.Log(data.id + " : " + data.name);
		}
	}
}

public class WebData<T>
{
	// 데이터 리스트
	public List<T> DataList = new List<T>();

	// 데이터 요청
	public IEnumerator Request(string url)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
		{
			// 요청 보내기
			yield return webRequest.SendWebRequest();

			// 요청이 완료되었는지 확인
			if (webRequest.result != UnityWebRequest.Result.Success)
			{
				// 에러 출력
				Debug.LogError("Error: " + webRequest.error);
			}
			else
			{
				// 요청 성공시 역직렬화하여 리스트에 저장
				string responseText = webRequest.downloadHandler.text;
				Debug.Log(responseText);
				DataList = JsonConvert.DeserializeObject<List<T>>(responseText);

				// 데이터 수신 확인
				Debug.Log($"데이터 {DataList.Count}개 수신 완료!");
				
			}
		}
	}

}

public class TempData // 테스트용 클래스
{
	public int postId;
	public int id;
	public string name;
	public string email;
	public string body;
}

public class EnviromentData // (미완) 데이터 클래스
{
	public string Time;
	public float Temperature;
	public float Humidity;
	public bool IsDangerous;
}
