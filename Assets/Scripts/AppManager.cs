using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
	// 추후 해당 메소드를 호출할때 데이터 저장이 모두 이루어지고 안전하게 종료되도록 만듬.
	// 평소에도 초단위로 데이터 자동 저장해서, 종료 방법에 상관없이 안전한 데이터 저장이 이루어지게 해야함.
	public void QuitApplication()
	{
		Application.Quit();
	}
}
