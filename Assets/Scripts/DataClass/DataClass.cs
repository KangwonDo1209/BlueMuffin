using System;
using System.Collections.Generic;
using System.Linq;

public class EnvironmentData
{
	public string Time;
	public int RoomId;
	public float Temperature;
	public float Humidity;
	public float Gas;
	public float Dust;
	public string DangerCode;


	public static DateTime ParseTime(string iso8601Time)
	{
		DateTime dateTime = DateTime.Parse(iso8601Time);
		return dateTime;
	}
	public static string ParseTime(DateTime dateTime)
	{
		string iso9601Time = dateTime.ToString("o");
		return iso9601Time;
	}
	public float GetDataByIndex(int index)
	{
		switch (index)
		{
			case 0:
				return Temperature;
			case 1:
				return Humidity;
			case 2:
				return Gas;
			case 3:
				return Dust;
			default:
				return 0;
		}
	}
	public string DataDisplay()
	{
		return $"Time: {Time}, RoomId: {RoomId}, Temperature: {Temperature}, Humidity: {Humidity}, Gas: {Gas}, Dust: {Dust}, DangerCode: {DangerCode}";
	}
}

public class RoomSensorData
{
	public int RoomIndex;
	public string RoomName;
	public string SensorCode;

	// 센서 코드 해석 메소드
	public bool[] ParseSensorCode()
	{
		int n = SensorCode.Length;
		bool[] parseCode = new bool[n];
		for (int i = 0; i < n; i++)
		{
			parseCode[i] = SensorCode[i] == '1';
		}
		return parseCode;
	}
	// 해당 index에 해당하는 RoomSensorData 불러오기
	public static RoomSensorData SearchDataByIndex(List<RoomSensorData> dataList, int index)
	{
		RoomSensorData roomSensorData = dataList.FirstOrDefault(data =>
		{
			return data.RoomIndex == index;
		});
		return roomSensorData;
	}

	
}

public class AduinoRequest
{
	public enum RequestType { Execute = 1, Terminate }

	public RequestType Type;
	public string Target;
	public int Value;
}