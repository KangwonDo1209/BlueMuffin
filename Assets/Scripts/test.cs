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
		// Scene 내에 있는 모든 GameObject 가져오기
		GameObject[] allGameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

		// GameObject를 순회하며 AppManager 스크립트를 가진 것을 찾음
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
