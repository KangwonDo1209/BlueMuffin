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
			Debug.Log(dataList.Count + "리스트");
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
	// 리스트의 데이터중 최근 1시간 데이터 필터링 후 반환
	public static List<EnviromentData> GetRecentData(List<EnviromentData> dataList, int minute)
	{
		DateTime currentTime = DateTime.Now;

		List<EnviromentData> recentData = dataList.Where(data =>
		{
			// 시간차 확인 후 
			TimeSpan timeDiff = currentTime - EnviromentData.ParseTime(data.Time);
			return (timeDiff.TotalMinutes) <= minute;
		}).ToList();

		return recentData;
	}
	// 리스트의 특정 속성 평균값을 반환.
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