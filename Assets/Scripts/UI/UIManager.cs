using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UI { Initial, Main, Data, Setting };
public class UIManager : MonoBehaviour
{
	public UI _UIState
	{
		get
		{
			return UIState;
		}
		set
		{

			//���� �� UICanvas Off

			UIState = value;

			//���� �� UICanvas On
			
		}
	}
	private UI UIState = UI.Initial;

	[SerializeField]
	private List<GameObject> PanelList = new List<GameObject>(); 
	


}
