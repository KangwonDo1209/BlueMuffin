using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class WebData<T>
{
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
				Debug.LogError("Error : " + webRequest.error);
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
