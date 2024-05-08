using UnityEditor.PackageManager.Requests;
using System;
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
	public int RoomId;
	public float Temperature;
	public float Humidity;
	public float Gas;
	public float Dust;
	public int DangerCode;


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
	public string DataDisplay()
	{
		return $"Time: {Time}, RoomId: {RoomId}, Temperature: {Temperature}, Humidity: {Humidity}, Gas: {Gas}, Dust: {Dust}, DangerCode: {DangerCode}";
	}
}

public class RoomSensorData
{
	public int RoomIndex;
	public int SensorCode;

	// 센서 코드 해석 메소드
}

public class AduinoRequest
{
	public enum RequestType { Execute = 1, Terminate }

	public RequestType Type;
	public string Target;
	public int Value;
}