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
		// ����Ʈ�� �������� ��谪�� ����Ͽ� EnvironmentData ���·� ������.
		EnvironmentData averageData = DataProcessor.CalculateDataMin(dataList, roomIndex);

		// roomIndex�� �ش��ϴ� RoomSensorData ��ü �ҷ�����
		RoomSensorData roomSensorData = WebManager.Instance.WebRoomSensorData.DataList.FirstOrDefault(data =>
		{
			return data.RoomIndex == roomIndex;
		});

		// RoomSensorData�� �����ڵ� �ؼ�
		bool[] parseCode = roomSensorData.ParseSensorCode();

		// �ؼ��� �ڵ忡 ���� ������ �ؽ�Ʈ GameObject Ȱ��ȭ/��Ȱ��ȭ
		for (int i = 1; i <= 4; i++)
		{
			TextList[i].gameObject.SetActive(parseCode[i - 1]);
		}


		// ������ �ؽ�Ʈ ����
		TextList[0].text = roomSensorData.RoomName;
		TextList[1].text = $"�µ� : {averageData.Temperature.ToString("N2")} ��C";
		TextList[2].text = $"���� : {averageData.Humidity.ToString("N2")} %";
		TextList[3].text = $"���� : {averageData.Gas.ToString("N2")} ppm";
		TextList[4].text = $"�̼����� : {averageData.Dust.ToString("N2")} ��/��";
		// TextList[5].text = $"���� ���� : {averageData.DangerCode}"; // (�ӽ�) �����ڵ忡 ���� ���� �ؽ�Ʈ �Լ� ���� ����
		TextList[5].text = $"�ֱ� {RecentMinute.ToString()}�� ���";


	}

}
