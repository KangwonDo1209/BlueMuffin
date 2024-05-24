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
	public bool PeriodicGenerating = false;
	Coroutine GenerateCouroutine;

	List<EnvironmentData> dataList;
	List<RoomSensorData> sensorDataList;

	void Start()
	{
		dataList = WebManager.Instance.WebEnviromentData.DataList;
		sensorDataList = WebManager.Instance.WebRoomSensorData.DataList;
	}

	void Update()
	{

	}

	#region TestCode
	public void PeriodicRandomData(string min_sec)
	{
		int[] parse = Array.ConvertAll(min_sec.Split(" "), int.Parse);
		int min = parse[0];
		int periodSec = parse[1];
		if (PeriodicGenerating)
		{
			StopCoroutine(GenerateCouroutine);
			PeriodicGenerating = false;
		}
		else
		{
			GenerateCouroutine = StartCoroutine(RandomData(min, periodSec));
			PeriodicGenerating = true;
		}

	}

	public IEnumerator RandomData(int min, int periodSec)
	{
		sensorDataList.Clear();
		sensorDataList.Add(new RoomSensorData() { RoomIndex = 1, RoomName = "Bedroom1", SensorCode = "1100" });
		sensorDataList.Add(new RoomSensorData() { RoomIndex = 2, RoomName = "Bedroom2", SensorCode = "1100" });
		sensorDataList.Add(new RoomSensorData() { RoomIndex = 3, RoomName = "Livingroom", SensorCode = "1111" });
		sensorDataList.Add(new RoomSensorData() { RoomIndex = 4, RoomName = "Restroom", SensorCode = "1100" });
		while (true)
		{
			WebManager.Instance.WebEnviromentData.DataList = new List<EnvironmentData>();
			dataList = WebManager.Instance.WebEnviromentData.DataList;
			for (int i = min; i >= 0; i--)
			{
				for (int roomNum = 1; roomNum <= 4; roomNum++)
				{
					dataList.Add(new EnvironmentData
					{
						Time = EnvironmentData.ParseTime(DateTime.Now.AddMinutes(-i)),
						RoomId = roomNum,
						Temperature = (float)UnityEngine.Random.Range(5, 25),
						Humidity = (float)UnityEngine.Random.Range(10, 80),
						Gas = (float)UnityEngine.Random.Range(10, 60),
						Dust = (float)UnityEngine.Random.Range(5, 60),
						DangerCode = "0000"
					});
				}
			}
			Debug.Log("Random!");
			yield return new WaitForSeconds(periodSec);
		}
	}
	#endregion

}

public class DataProcessor
{
	// 최근 minute 데이터 불러오기
	public static List<EnvironmentData> GetRecentData(List<EnvironmentData> dataList, int roomIndex, int minute)
	{
		DateTime currentTime = DateTime.Now;

		List<EnvironmentData> recentData = dataList.Where(data =>
		{
			TimeSpan timeDiff = currentTime - EnvironmentData.ParseTime(data.Time);
			return (timeDiff.TotalMinutes) <= minute && data.RoomId == roomIndex;
		}).ToList();

		return recentData;
	}
	public static List<DateTime> FilterDateTimes(List<DateTime> dateTimes, DateTime start, DateTime end)
	{
		// LINQ를 사용하여 start와 end 사이의 값만 필터링
		return dateTimes.Where(dt => dt >= start && dt <= end).ToList();
	}

	// start부터 end까지의 데이터 리스트 불러오기
	public static List<EnvironmentData> GetRangeData(List<EnvironmentData> dataList, int roomIndex, DateTime start, DateTime end)
	{

		List<EnvironmentData> rangeData = dataList.Where(data =>
		{
			DateTime dt = EnvironmentData.ParseTime(data.Time);
			return (dt >= start && dt <= end) && data.RoomId == roomIndex;
		}).ToList();

		return rangeData;
	}
	public static EnvironmentData CalculateByIndex(List<EnvironmentData> dataList, int roomIndex, int index)
	{
		switch (index)
		{
			case 0:
				return CalculateDataAverage(dataList, roomIndex);
			case 1:
				return CalculateDataMax(dataList, roomIndex);
			case 2:
				return CalculateDataMin(dataList, roomIndex);
			default:
				return CalculateDataAverage(dataList, roomIndex); // 잘못된 index일시 평균 반환
		}
	}
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
			Time = "Average",
			RoomId = roomIndex,
			Temperature = averageTemperature,
			Humidity = averageHumidity,
			Gas = averageGas,
			Dust = averageDust,
			DangerCode = "0000"
		};
		return averageData;
	}
	public static EnvironmentData CalculateDataMax(List<EnvironmentData> dataList, int roomIndex)
	{
		if (dataList.Count == 0)
			return new EnvironmentData();

		float MaxTemperature = -500;
		float MaxHumidity = 0;
		float MaxGas = 0;
		float MaxDust = 0;
		int count = dataList.Count;

		foreach (EnvironmentData data in dataList)
		{
			MaxTemperature = math.max(MaxTemperature, data.Temperature);
			MaxHumidity = math.max(MaxHumidity, data.Humidity);
			MaxGas = math.max(MaxGas, data.Gas);
			MaxDust = math.max(MaxDust, data.Dust);
		}

		EnvironmentData MaxData = new EnvironmentData
		{
			Time = "Max",
			RoomId = roomIndex,
			Temperature = MaxTemperature,
			Humidity = MaxHumidity,
			Gas = MaxGas,
			Dust = MaxDust,
			DangerCode = "0000"
		};
		return MaxData;
	}
	public static EnvironmentData CalculateDataMin(List<EnvironmentData> dataList, int roomIndex)
	{
		if (dataList.Count == 0)
			return new EnvironmentData();

		float MinTemperature = 10000;
		float MinHumidity = 10000;
		float MinGas = 10000;
		float MinDust = 10000;
		int count = dataList.Count;

		foreach (EnvironmentData data in dataList)
		{
			MinTemperature = math.min(MinTemperature, data.Temperature);
			MinHumidity = math.min(MinHumidity, data.Humidity);
			MinGas = math.min(MinGas, data.Gas);
			MinDust = math.min(MinDust, data.Dust);
		}

		EnvironmentData MinData = new EnvironmentData
		{
			Time = "Max",
			RoomId = roomIndex,
			Temperature = MinTemperature,
			Humidity = MinHumidity,
			Gas = MinGas,
			Dust = MinDust,
			DangerCode = "0000"
		};
		return MinData;
	}


}