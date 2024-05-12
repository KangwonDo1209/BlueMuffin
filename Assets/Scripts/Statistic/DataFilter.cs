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

		sensorDataList.Add(new RoomSensorData() { RoomIndex = 1, RoomName = "ħ��1", SensorCode = "1100" });
		sensorDataList.Add(new RoomSensorData() { RoomIndex = 2, RoomName = "ħ��2", SensorCode = "1100" });
		sensorDataList.Add(new RoomSensorData() { RoomIndex = 3, RoomName = "�ֹ�", SensorCode = "1111" });
		sensorDataList.Add(new RoomSensorData() { RoomIndex = 4, RoomName = "���", SensorCode = "1100" });
	}

	void Update()
	{

	}

	#region �׽�Ʈ�� ���� ������
	public void PeriodicRandomDat(string min_sec)    // ���� ������ ����
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
		while (true)
		{
			WebManager.Instance.WebEnviromentData.DataList = new List<EnvironmentData>();
			dataList = WebManager.Instance.WebEnviromentData.DataList;
			for (int i = min; i >= 0; i--)
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
			Debug.Log("Random!");
			yield return new WaitForSeconds(periodSec);
		}
	} 
	#endregion

}

public class DataProcessor
{
	// ����Ʈ�� ������ �� Ư�� ���� �ֱ� ������ ���͸� �� ��ȯ
	public static List<EnvironmentData> GetRecentData(List<EnvironmentData> dataList, int roomIndex, int minute)
	{
		DateTime currentTime = DateTime.Now;

		List<EnvironmentData> recentData = dataList.Where(data =>
		{
			// �ð��� Ȯ�� �� 
			TimeSpan timeDiff = currentTime - EnvironmentData.ParseTime(data.Time);
			return (timeDiff.TotalMinutes) <= minute && data.RoomId == roomIndex;
		}).ToList();

		return recentData;
	}
	// ����Ʈ�� Ư�� �Ӽ� ��հ��� ��ȯ.
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

		// ǥ�ø� ���� �ֱ� ��� ������
		EnvironmentData averageData = new EnvironmentData
		{
			Time = "Average", // �ӽ�
			RoomId = roomIndex, // �Է°����� ���� ��ȣ
			Temperature = averageTemperature,
			Humidity = averageHumidity,
			Gas = averageGas,
			Dust = averageDust,
			DangerCode = "0000" // �ӽ�
		};

		return averageData;
	}



}