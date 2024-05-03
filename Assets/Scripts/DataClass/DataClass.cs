using UnityEditor.PackageManager.Requests;
using System;
public class TempData // �׽�Ʈ�� Ŭ����
{
	public int postId;
	public int id;
	public string name;
	public string email;
	public string body;
}

public class EnviromentData // (�̿�) ������ Ŭ����
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