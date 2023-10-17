using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UI { Initial = 1, Main, Data, Setting };
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
			for (int i = 1; i < PanelList.Count; i++)
			{
				DisablePanel(PanelList[i]);
			}
			//변경 전 UIPanel Off

			UIState = value;

			//변경 후 UIPanel On
			EnablePanel(PanelList[(int)UIState]);
			_DebugPanelScript.CurrentUI.text = _DebugPanelScript.defaultCurrentUIString +  _UIState.ToString();
		}
	}
	private UI UIState = UI.Initial;

	[SerializeField]
	private List<GameObject> PanelList = new List<GameObject>();

	private DebugPanel _DebugPanelScript;

	private void Start()
	{
		_DebugPanelScript = GameObject.Find("DebugPanel").GetComponent<DebugPanel>();
	}

	public void DisablePanel(GameObject panel)
	{
		panel.SetActive(false);
	}
	public void EnablePanel(GameObject panel)
	{
		panel.SetActive(true);
	}
	public void ChangePanel(int index)
	{
		_UIState = (UI)index;
	}
}
