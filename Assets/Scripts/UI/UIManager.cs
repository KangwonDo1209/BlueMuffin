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

			//변경 전 UICanvas Off

			UIState = value;

			//변경 후 UICanvas On
			
		}
	}
	private UI UIState = UI.Initial;

	[SerializeField]
	private List<GameObject> PanelList = new List<GameObject>(); 
	


}
