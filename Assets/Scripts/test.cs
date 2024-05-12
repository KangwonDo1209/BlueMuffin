using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	private void Awake()
	{
		var r = CountGameObjectsWithAppManagerScript();
	}
	public int CountGameObjectsWithAppManagerScript()
	{
		int count = 0;
		// Scene ���� �ִ� ��� GameObject ��������
		GameObject[] allGameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

		// GameObject�� ��ȸ�ϸ� AppManager ��ũ��Ʈ�� ���� ���� ã��
		foreach (GameObject go in allGameObjects)
		{
			if (go.GetComponent<AppManager>() != null)
			{
				count++;
			}
		}

		return count;
	}
}
