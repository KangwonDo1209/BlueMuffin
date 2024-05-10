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

	// ���̸�/�µ�/����/����/�̼�����/���迩��
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
		// roomIndex�� �ش��ϴ� ���� �ֱ� minute������ EnvironmentData�� ����Ʈ�� ������.
		List<EnvironmentData> dataList = DataProcessor.GetRecentData(WebManager.Instance.WebEnviromentData.DataList, roomIndex, minute);
		// ����Ʈ�� ������ ����� ����Ͽ� EnvironmentData ���·� ������.
		EnvironmentData averageData = DataProcessor.CalculateDataAverage(dataList, roomIndex);

		// �ش�Ǵ� �����͸� ȭ�鿡 ǥ������.
		TextList[0].text = WebManager.Instance.WebRoomSensorData.DataList.FirstOrDefault(data =>
		{
			return data.RoomIndex == roomIndex;
		}).RoomName;


		
	}

}
