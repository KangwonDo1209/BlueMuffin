using UnityEngine;

public class GraphDrawer : MonoBehaviour
{
	public LineRenderer lineRenderer; // 그래프를 그릴 LineRenderer
	public float[] data; // 입력 데이터

	void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	void Start()
	{
		// LineRenderer의 위치 개수를 데이터의 크기로 설정
		lineRenderer.positionCount = data.Length;

		// 입력 데이터를 LineRenderer의 각 지점에 할당
		for (int i = 0; i < data.Length; i++)
		{
			lineRenderer.SetPosition(i, new Vector3(i, data[i], 0));
			// i는 x 좌표, data[i]는 y 좌표, 0은 z 좌표
		}
	}
}
