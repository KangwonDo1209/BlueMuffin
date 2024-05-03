using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class DataFilter : MonoBehaviour
{

	List<EnviromentData> dataList;

	void Start()
	{
		dataList = new List<EnviromentData>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.V))
		{
			for (int i = 18000; i >= 0; i--)
			{
				dataList.Add(new EnviromentData
				{
					Time = DateTime.Now.AddMinutes(-i),
					Temperature = (float)UnityEngine.Random.Range(5, 25),
					Humidity = (float)UnityEngine.Random.Range(10, 80),
					IsDangerous = false
				});
			}

			var list = DataProcessor.GetRecentData(dataList, 3000);
			string type = "Temperature";
			float avg = DataProcessor.CalculateDataAverage(list, type);
			Debug.Log(list.Count + " - " + type + " : " + avg);
		}
	}
}

public class DataProcessor
{
	// ����Ʈ�� �������� �ֱ� 1�ð� ������ ���͸� �� ��ȯ
	public static List<EnviromentData> GetRecentData(List<EnviromentData> dataList, int minute)
	{
		DateTime currentTime = DateTime.Now;

		List<EnviromentData> recentData = dataList.Where(data =>
		{
			// �ð��� Ȯ�� �� 
			TimeSpan timeDiff = currentTime - data.Time;
			return (timeDiff.TotalMinutes) <= minute;
		}).ToList();

		return recentData;
	}
	// ����Ʈ�� Ư�� �Ӽ� ��հ��� ��ȯ.
	public static float CalculateDataAverage(List<EnviromentData> dataList, string fieldName)
	{
		if (dataList.Count == 0)
		{
			return float.MinValue;
		}
		float sum = 0;


		switch (fieldName.ToLower())
		{
			case "temperature":
				foreach (var item in dataList)
				{
					sum += item.Temperature;
				}
				break;
			case "humidity":
				foreach (var item in dataList)
				{
					sum += item.Humidity;
				}
				break;
			case "isdangerous":
				foreach (var item in dataList)
				{
					if (item.IsDangerous)
						sum += 1f;
				}
				break;
			default:
				Debug.Log(fieldName.ToLower() + " is Not Field");
				return float.MinValue;
		}

		float average = sum / dataList.Count;

		return average;
	}


}