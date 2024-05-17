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
	// �µ�/����/����/�̼�����
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
	// ���� ���� ���� (�µ�/����/����/�̼�����)
	public void SelectInfo(int infoInput)
	{
		// infoInput ��ȿ�� �˻�
		if (!validationRange(infoInput, 1, 4))
			return;
		// info ����
		info = infoInput;
		// InfoText ����
		string t = StatTextList[info].text;
		ChangeText(InfoText, t);
	}
	// ��� ���� ���� (���/�ִ밪/�ּҰ�)
	public void SelectStat(int statInput)
	{
		// statInput ��ȿ�� �˻�
		if (!validationRange(statInput, 1, 4))
			return;
		// stat ����
	}
	//  ������ ���� �ð� �Է� (���� : ��)
	public void InputTimeRange()
	{
		// �Է°� �ҷ�����
		int start = 1;
		int end = 3;
		// ��ȿ�� �˻�
		
		// MinuteRange ����
	}
	// ������ ǥ��(��ư Ŭ���� �۵�)
	public void ClickShowStat()
	{
		// 3���� ���ð��� ���߾�, ������ ����(���ϴ� ������ ����/��� ����/�ð� ����)

		// ���갪 ǥ��


	}
	// 



}
