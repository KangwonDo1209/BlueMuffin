using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Chapter.Singleton;
using System.Linq;
using System;


public class InteractStatstic : Singleton<InteractStatstic>
{

	public int minuteRangeStart;
	public int minuteRangeEnd;
	public int info;
	public int stat;
	private int RoomCount;
	[SerializeField]
	private TMP_Text InfoText;
	[SerializeField]
	private TMP_Text StatText;

	[SerializeField]
	private List<GameObject> StatTextObjectList = new List<GameObject>();
	private List<TMP_Text> StatTextList = new List<TMP_Text>();
	[SerializeField]
	private List<GameObject> RoomTextObjectList = new List<GameObject>();
	private List<TMP_Text> RoomTextList = new List<TMP_Text>();
	// 온도/습도/가스/미세먼지
	public override void Awake()
	{
		base.Awake();

		foreach (GameObject item in StatTextObjectList)
		{
			StatTextList.Add(item.GetComponent<TMP_Text>());
		}
		foreach (GameObject item in RoomTextObjectList)
		{
			RoomTextList.Add(item.GetComponent<TMP_Text>());
		}
	}
	public bool validationRange(int value, int start, int end)
	{
		if (value >= start && value <= end)
			return true;
		else
			return false;
	}
	public void ChangeText(TMP_Text TextObject, string text)
	{
		TextObject.text = text;
	}
	// 정보 종류 선택 (온도/습도/가스/미세먼지)
	public void SelectInfo(int infoInput)
	{
		// infoInput 유효성 검사
		if (!validationRange(infoInput, 1, 4))
			return;
		// info 변경
		info = infoInput;
		// InfoText 변경
		string t = StatTextList[info].text;
		ChangeText(InfoText, t);
	}
	// 통계 종류 선택 (평균/최대값/최소값)
	public void SelectStat(int statInput)
	{
		// statInput 유효성 검사
		if (!validationRange(statInput, 1, 4))
			return;
		// stat 변경
	}
	//  데이터 범위 시간 입력 (단위 : 분)
	public void InputTimeRange()
	{
		// 입력값 불러오기
		int start = 1;
		int end = 3;
		// 유효값 검사
		
		// MinuteRange 변경
	}
	// 데이터 표시(버튼 클릭시 작동)
	public void ClickShowStat()
	{
		// 3가지 선택값에 맞추어, 데이터 연산(원하는 데이터 종류/통계 종류/시간 범위)

		// 연산값 표시


	}
	// 



}
