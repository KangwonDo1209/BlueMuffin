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

	List<EnvironmentData> dataList;
	List<RoomSensorData> sensorDataList;

	void Start()
	{
		dataList = WebManager.Instance.WebEnviromentData.DataList;
		sensorDataList = WebManager.Instance.WebRoomSensorData.DataList;

		sensorDataList.Add(new RoomSensorData() { RoomIndex = 1, RoomName = "침실1", SensorCode = "1100" });
		sensorDataList.Add(new RoomSensorData() { RoomIndex = 2, RoomName = "침실2", SensorCode = "1100" });
		sensorDataList.Add(new RoomSensorData() { RoomIndex = 3, RoomName = "주방", SensorCode = "1111" });
		sensorDataList.Add(new RoomSensorData() { RoomIndex = 4, RoomName = "욕실", SensorCode = "1100" });
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Y))
			Debug.Log(dataList.Count + "리스트");
		if (Input.GetKeyDown(KeyCode.V))
		{
			for (int i = 3600; i >= 0; i--)
			{
				dataList.Add(new EnvironmentData
				{
					Time = EnvironmentData.ParseTime(DateTime.Now.AddMinutes(-i)),
					RoomId = UnityEngine.Random.Range(1, 5),
					Temperature = (float)UnityEngine.Random.Range(5, 25),
					Humidity = (float)UnityEngine.Random.Range(10, 80),
					Gas = (float)UnityEngine.Random.Range(10, 60),
					Dust = (float)UnityEngine.Random.Range(5, 60),
					DangerCode = "0000"
				});
			}

			var list = DataProcessor.GetRecentData(dataList, 1, 30);
			EnvironmentData avg = DataProcessor.CalculateDataAverage(list, 1);
			Debug.Log(list.Count + " : " + avg.DataDisplay());
		}
	}
}

public class DataProcessor
{
	// 리스트의 데이터 중 특정 방의 최근 데이터 필터링 후 반환
	public static List<EnvironmentData> GetRecentData(List<EnvironmentData> dataList, int roomIndex, int minute)
	{
		DateTime currentTime = DateTime.Now;

		List<EnvironmentData> recentData = dataList.Where(data =>
		{
			// 시간차 확인 후 
			TimeSpan timeDiff = currentTime - EnvironmentData.ParseTime(data.Time);
			return (timeDiff.TotalMinutes) <= minute && data.RoomId == roomIndex;
		}).ToList();

		return recentData;
	}
	// 리스트의 특정 속성 평균값을 반환.
	public static EnvironmentData CalculateDataAverage(List<EnvironmentData> dataList, int roomIndex)
	{
		if (dataList.Count == 0)
			return new EnvironmentData();

		float totalTemperature = 0;
		float totalHumidity = 0;
		float totalGas = 0;
		float totalDust = 0;
		int count = dataList.Count;

		foreach (EnvironmentData data in dataList)
		{
			totalTemperature += data.Temperature;
			totalHumidity += data.Humidity;
			totalGas += data.Gas;
			totalDust += data.Dust;
		}
		float averageTemperature = totalTemperature / count;
		float averageHumidity = totalHumidity / count;
		float averageGas = totalGas / count;
		float averageDust = totalDust / count;


		EnvironmentData averageData = new EnvironmentData
		{
			Time = "Average", // 임시
			RoomId = roomIndex, // 입력값으로 받은 번호
			Temperature = averageTemperature,
			Humidity = averageHumidity,
			Gas = averageGas,
			Dust = averageDust,
			DangerCode = "0000" // 임시
		};

		return averageData;
	}

}