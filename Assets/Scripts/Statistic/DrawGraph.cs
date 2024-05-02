using UnityEngine;

public class GraphDrawer : MonoBehaviour
{
	public LineRenderer lineRenderer; // �׷����� �׸� LineRenderer
	public float[] data; // �Է� ������

	void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	void Start()
	{
		// LineRenderer�� ��ġ ������ �������� ũ��� ����
		lineRenderer.positionCount = data.Length;

		// �Է� �����͸� LineRenderer�� �� ������ �Ҵ�
		for (int i = 0; i < data.Length; i++)
		{
			lineRenderer.SetPosition(i, new Vector3(i, data[i], 0));
			// i�� x ��ǥ, data[i]�� y ��ǥ, 0�� z ��ǥ
		}
	}
}
