using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
	// ���� �ش� �޼ҵ带 ȣ���Ҷ� ������ ������ ��� �̷������ �����ϰ� ����ǵ��� ����.
	// ��ҿ��� �ʴ����� ������ �ڵ� �����ؼ�, ���� ����� ������� ������ ������ ������ �̷������ �ؾ���.
	public void QuitApplication()
	{
		Application.Quit();
	}
	public void SceneReset()
	{
		SceneManager.LoadScene(0);
	}
}
