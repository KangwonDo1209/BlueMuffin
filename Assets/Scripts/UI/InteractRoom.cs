using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Chapter.Singleton;
using System.Linq;


public class InteractRoom : Singleton<InteractRoom>
{
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

	public void LoadRoomData(int roomIndex, int minute)
	{
		// roomIndex에 해당하는 방의 최근 minute동안의 EnvironmentData를 리스트로 가져옴.
		List<EnvironmentData> dataList = DataProcessor.GetRecentData(WebManager.Instance.WebEnviromentData.DataList, roomIndex, minute);
		// 리스트의 데이터 평균을 계산하여 EnvironmentData 형태로 가져옴.
		EnvironmentData averageData = DataProcessor.CalculateDataAverage(dataList, roomIndex);

		// 해당되는 데이터를 화면에 표시해줌.
		TextList[0].text = WebManager.Instance.WebRoomSensorData.DataList.FirstOrDefault(data =>
		{
			return data.RoomIndex == roomIndex;
		}).RoomName;


		
	}

}
