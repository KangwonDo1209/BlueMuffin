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
		if (Input.GetKeyDown(KeyCode.Y))
			Debug.Log(dataList.Count + "����Ʈ");
		if (Input.GetKeyDown(KeyCode.V))
		{
			for (int i = 18000; i >= 0; i--)
			{
				dataList.Add(new EnviromentData
				{
					Time = EnviromentData.ParseTime(DateTime.Now.AddMinutes(-i)),
					RoomId = UnityEngine.Random.Range(1, 5),
					Temperature = (float)UnityEngine.Random.Range(5, 25),
					Humidity = (float)UnityEngine.Random.Range(10, 80),
					Gas = (float)UnityEngine.Random.Range(10, 60),
					Dust = (float)UnityEngine.Random.Range(5, 60),
					DangerCode = UnityEngine.Random.Range(0, 40)
				});
			}

			var list = DataProcessor.GetRecentData(dataList, 3000);
			EnviromentData avg = DataProcessor.CalculateDataAverage(list);
			Debug.Log(list.Count + " : " + avg.DataDisplay());
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
			TimeSpan timeDiff = currentTime - EnviromentData.ParseTime(data.Time);
			return (timeDiff.TotalMinutes) <= minute;
		}).ToList();

		return recentData;
	}
	// ����Ʈ�� Ư�� �Ӽ� ��հ��� ��ȯ.
	public static EnviromentData CalculateDataAverage(List<EnviromentData> dataList)
	{
		if (dataList.Count == 0)
			return new EnviromentData();

		float totalTemperature = 0;
		float totalHumidity = 0;
		float totalGas = 0;
		float totalDust = 0;
		int totalDangerCode = 0;
		int count = dataList.Count;

		foreach (EnviromentData data in dataList)
		{
			totalTemperature += data.Temperature;
			totalHumidity += data.Humidity;
			totalGas += data.Gas;
			totalDust += data.Dust;
			totalDangerCode += data.DangerCode;
		}
		float averageTemperature = totalTemperature / count;
		float averageHumidity = totalHumidity / count;
		float averageGas = totalGas / count;
		float averageDust = totalDust / count;
		int averageDangerCode = totalDangerCode / count;

		EnviromentData averageData = new EnviromentData
		{
			Time = "Average",
			RoomId = 0,
			Temperature = averageTemperature,
			Humidity = averageHumidity,
			Gas = averageGas,
			Dust = averageDust,
			DangerCode = averageDangerCode
		};

		return averageData;
	}

}