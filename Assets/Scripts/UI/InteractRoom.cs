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

	// 방이름/온도/습도/가스/미세먼지/위험여부
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
		// roomIndex에 해당하는 방의 최근 minute동안의 EnvironmentData를 리스트로 가져옴.
		List<EnvironmentData> dataList = DataProcessor.GetRecentData(WebManager.Instance.WebEnviromentData.DataList, roomIndex, RecentMinute);
		// 리스트의 데이터의 통계값을 계산하여 EnvironmentData 형태로 가져옴.
		EnvironmentData averageData = DataProcessor.CalculateDataMin(dataList, roomIndex);

		// roomIndex에 해당하는 RoomSensorData 객체 불러오기
		RoomSensorData roomSensorData = WebManager.Instance.WebRoomSensorData.DataList.FirstOrDefault(data =>
		{
			return data.RoomIndex == roomIndex;
		});

		// RoomSensorData의 센서코드 해석
		bool[] parseCode = roomSensorData.ParseSensorCode();

		// 해석한 코드에 따라 데이터 텍스트 GameObject 활성화/비활성화
		for (int i = 1; i <= 4; i++)
		{
			TextList[i].gameObject.SetActive(parseCode[i - 1]);
		}


		// 데이터 텍스트 변경
		TextList[0].text = roomSensorData.RoomName;
		TextList[1].text = $"온도 : {averageData.Temperature.ToString("N2")} °C";
		TextList[2].text = $"습도 : {averageData.Humidity.ToString("N2")} %";
		TextList[3].text = $"가스 : {averageData.Gas.ToString("N2")} ppm";
		TextList[4].text = $"미세먼지 : {averageData.Dust.ToString("N2")} ㎍/㎥";
		// TextList[5].text = $"위험 여부 : {averageData.DangerCode}"; // (임시) 위험코드에 따른 위험 텍스트 함수 제작 예정
		TextList[5].text = $"최근 {RecentMinute.ToString()}분 평균";


	}

}
