using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Chapter.Singleton;
using System.Linq;
using System;


public class InteractRoom : Singleton<InteractRoom>
{
	public int RecentMinute;

	[SerializeField]
	private List<GameObject> TextObjectList = new List<GameObject>();
	private List<TMP_Text> TextList = new List<TMP_Text>();

	// ���̸�/�µ�/����/����/�̼�����/���迩��
	public override void Awake()
	{
		base.Awake();

		foreach (GameObject item in TextObjectList)
		{
			TextList.Add(item.GetComponent<TMP_Text>());
		}
	}

	public void LoadRoomData(int roomIndex)
	{
		// roomIndex�� �ش��ϴ� ���� �ֱ� minute������ EnvironmentData�� ����Ʈ�� ������.
		List<EnvironmentData> dataList = DataProcessor.GetRecentData(WebManager.Instance.WebEnviromentData.DataList, roomIndex, RecentMinute);
		// ����Ʈ�� ������ ����� ����Ͽ� EnvironmentData ���·� ������.
		EnvironmentData averageData = DataProcessor.CalculateDataAverage(dataList, roomIndex);

		// �ش�Ǵ� �����͸� ȭ�鿡 ǥ������.
		TextList[0].text = WebManager.Instance.WebRoomSensorData.DataList.FirstOrDefault(data =>
		{
			return data.RoomIndex == roomIndex;
		}).RoomName;
		TextList[1].text = $"�µ� : {averageData.Temperature.ToString()} ��C";
		TextList[2].text = $"���� : {averageData.Humidity.ToString()} %";
		TextList[3].text = $"���� : {averageData.Gas.ToString()} ppm";
		TextList[4].text = $"�̼����� : {averageData.Dust.ToString()} ��/��";
		TextList[5].text = $"���� ���� : {averageData.DangerCode}"; // (�ӽ�) �����ڵ忡 ���� ���� �ؽ�Ʈ �Լ� ���� ����



	}

}
