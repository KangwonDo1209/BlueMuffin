using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Chapter.Singleton;
using System.Linq;
using System;


public class InteractStatstic : Singleton<InteractStatstic>
{
	#region ���� ����
	public int minuteRangeStart;
	public int minuteRangeEnd;
	public int info;
	public int stat;
	private int RoomCount;
	[SerializeField]
	private TMP_Text InfoText;
	[SerializeField]
	private TMP_Text StatText;

	public TMP_InputField FilterMinuteStartField;
	public TMP_InputField FilterMinuteEndField;
	public TMP_Dropdown StatDropDown;

	// ��跮/�̸� ���� ������Ʈ
	public List<GameObject> StatObjectList = new List<GameObject>();
	// �� ��跮 �ؽ�Ʈ
	public List<GameObject> StatTextObjectList = new List<GameObject>();
	private List<TMP_Text> StatTextList = new List<TMP_Text>();
	// �� �̸� �ؽ�Ʈ
	public List<GameObject> RoomTextObjectList = new List<GameObject>();
	private List<TMP_Text> RoomTextList = new List<TMP_Text>();
	// �µ�/����/����/�̼�����
	public List<TMP_Text> InfoButtonTextList = new List<TMP_Text>();

	#endregion

	#region ��ó�� ���
	public override void Awake()
	{
		base.Awake();


	}
	private void Start()
	{
		foreach (GameObject item in StatTextObjectList)
		{
			StatTextList.Add(item.GetComponent<TMP_Text>());
		}
		foreach (GameObject item in RoomTextObjectList)
		{
			RoomTextList.Add(item.GetComponent<TMP_Text>());
		}

	}
	#endregion

	#region ���� ���� ���
	public bool validationRange(int value, int start, int end)
	{
		return value >= start && value <= end;
	}
	public bool CanFilterData()
	{
		return validationRange(info, 0, 3) && validationRange(stat, 0, 2) && minuteRangeStart >= minuteRangeEnd;
	}
	#endregion

	#region �Է±��

	// ���� ���� ���� (�µ�/����/����/�̼�����)
	public void SelectInfo(int infoInput)
	{
		// infoInput ��ȿ�� �˻�
		if (!validationRange(infoInput, 0, 3))
			return;
		// info ����
		info = infoInput;
		// InfoText ����
		string t = InfoButtonTextList[info].text;
		ChangeText(InfoText, t);
	}
	// ��� ���� ���� (���/�ִ밪/�ּҰ�)
	public void SelectStat()
	{
		// stat ����
		stat = StatDropDown.value;
		string t = StatDropDown.options[stat].text;
		ChangeText(StatText, t);
	}
	//  ������ ���� �ð� �Է� (���� : ��)
	public bool InputTimeRange()
	{
		int start, end;
		// �Է°� �ҷ����� / ��ȿ�� �˻�
		if (!int.TryParse(FilterMinuteStartField.text, out start))
			return false;
		if (!int.TryParse(FilterMinuteEndField.text, out end))
			return false;

		// MinuteRange ����
		minuteRangeStart = start;
		minuteRangeEnd = end;
		return true;
	}
	#endregion

	#region ������ ��� ���
	// �ؽ�Ʈ ����
	public void ChangeText(TMP_Text TextObject, string text)
	{
		TextObject.text = text;
	}
	// ������ ǥ��(���� ��ǥ ���)
	public void ClickShowStat()
	{
		if (!InputTimeRange()) // �ð� �Է� �� ����
			return;
		if (!CanFilterData())
			return;

		// �� ���� �ҷ�����
		RoomCount = WebManager.Instance.WebRoomSensorData.DataList.Count;
		// 3���� ���ð��� ���߾�, ������ ���͸�(���ϴ� ������ ����/��� ����/�ð� ����)
		for (int roomIndex = 1; roomIndex <= RoomCount; roomIndex++)
		{
			// i ���� RoomSensorCode
			RoomSensorData sensorData = RoomSensorData.SearchDataByIndex(WebManager.Instance.WebRoomSensorData.DataList, roomIndex);
			bool[] sensorCode = sensorData.ParseSensorCode();
			bool s = sensorCode[info];
			// i ���� RoomSensorCode�� 0�̶�� �ش� �κ� ǥ�� X
			StatObjectList[roomIndex - 1].SetActive(s);

			if (!s)
				continue;

			// �ð�
			List<EnvironmentData> dataList = DataProcessor.GetRangeData(WebManager.Instance.WebEnviromentData.DataList, roomIndex, minuteRangeStart, minuteRangeEnd);
			// ��� ����
			EnvironmentData data = DataProcessor.CalculateByIndex(dataList, roomIndex, stat);
			// ������ ����
			float f = data.GetDataByIndex(info);

			// �ؽ�Ʈ ����
			ChangeText(RoomTextList[roomIndex - 1], sensorData.RoomName);
			ChangeText(StatTextList[roomIndex - 1], f.ToString("N2"));
		}
	}
	#endregion



}
