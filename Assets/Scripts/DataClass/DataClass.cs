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
	public DateTime Time;
	public float Temperature;
	public float Humidity;
	public bool IsDangerous;

}

public class AduinoRequest
{
	public enum RequestType { Execute = 1, Terminate }

	public RequestType Type;
	public string Target;
	public int Value;
}