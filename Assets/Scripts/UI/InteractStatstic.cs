using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Chapter.Singleton;
using System.Linq;
using System;


public class InteractStatstic : Singleton<InteractStatstic>
{
	#region 변수 정의
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

	// 통계량/이름 통합 오브젝트
	public List<GameObject> StatObjectList = new List<GameObject>();
	// 방 통계량 텍스트
	public List<GameObject> StatTextObjectList = new List<GameObject>();
	private List<TMP_Text> StatTextList = new List<TMP_Text>();
	// 방 이름 텍스트
	public List<GameObject> RoomTextObjectList = new List<GameObject>();
	private List<TMP_Text> RoomTextList = new List<TMP_Text>();
	// 온도/습도/가스/미세먼지
	public List<TMP_Text> InfoButtonTextList = new List<TMP_Text>();

	#endregion

	#region 전처리 기능
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

	#region 오류 방지 기능
	public bool validationRange(int value, int start, int end)
	{
		return value >= start && value <= end;
	}
	public bool CanFilterData()
	{
		return validationRange(info, 0, 3) && validationRange(stat, 0, 2) && minuteRangeStart >= minuteRangeEnd;
	}
	#endregion

	#region 입력기능

	// 정보 종류 선택 (온도/습도/가스/미세먼지)
	public void SelectInfo(int infoInput)
	{
		// infoInput 유효성 검사
		if (!validationRange(infoInput, 0, 3))
			return;
		// info 변경
		info = infoInput;
		// InfoText 변경
		string t = InfoButtonTextList[info].text;
		ChangeText(InfoText, t);
	}
	// 통계 종류 선택 (평균/최대값/최소값)
	public void SelectStat()
	{
		// stat 변경
		stat = StatDropDown.value;
		string t = StatDropDown.options[stat].text;
		ChangeText(StatText, t);
	}
	//  데이터 범위 시간 입력 (단위 : 분)
	public bool InputTimeRange()
	{
		int start, end;
		// 입력값 불러오기 / 유효성 검사
		if (!int.TryParse(FilterMinuteStartField.text, out start))
			return false;
		if (!int.TryParse(FilterMinuteEndField.text, out end))
			return false;

		// MinuteRange 변경
		minuteRangeStart = start;
		minuteRangeEnd = end;
		return true;
	}
	#endregion

	#region 데이터 출력 기능
	// 텍스트 변경
	public void ChangeText(TMP_Text TextObject, string text)
	{
		TextObject.text = text;
	}
	// 데이터 표시(최종 목표 기능)
	public void ClickShowStat()
	{
		if (!InputTimeRange()) // 시간 입력 및 시작
			return;
		if (!CanFilterData())
			return;

		// 방 개수 불러오기
		RoomCount = WebManager.Instance.WebRoomSensorData.DataList.Count;
		// 3가지 선택값에 맞추어, 데이터 필터링(원하는 데이터 종류/통계 종류/시간 범위)
		for (int roomIndex = 1; roomIndex <= RoomCount; roomIndex++)
		{
			// i 방의 RoomSensorCode
			RoomSensorData sensorData = RoomSensorData.SearchDataByIndex(WebManager.Instance.WebRoomSensorData.DataList, roomIndex);
			bool[] sensorCode = sensorData.ParseSensorCode();
			bool s = sensorCode[info];
			// i 방의 RoomSensorCode가 0이라면 해당 부분 표시 X
			StatObjectList[roomIndex - 1].SetActive(s);

			if (!s)
				continue;

			// 시간
			List<EnvironmentData> dataList = DataProcessor.GetRangeData(WebManager.Instance.WebEnviromentData.DataList, roomIndex, minuteRangeStart, minuteRangeEnd);
			// 통계 종류
			EnvironmentData data = DataProcessor.CalculateByIndex(dataList, roomIndex, stat);
			// 데이터 종류
			float f = data.GetDataByIndex(info);

			// 텍스트 변경
			ChangeText(RoomTextList[roomIndex - 1], sensorData.RoomName);
			ChangeText(StatTextList[roomIndex - 1], f.ToString("N2"));
		}
	}
	#endregion



}
