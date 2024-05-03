using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class WebData<T>
{
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
				Debug.LogError("Error : " + webRequest.error);
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
