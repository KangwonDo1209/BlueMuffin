using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UI { Initial = 1, Main, Data, Setting };
public class UIManager : MonoBehaviour
{
	private bool AlwaysUIEnabled;
	public bool _AlwaysUIEnabled
	{
		get
		{
			return AlwaysUIEnabled;
		}
		set
		{
			AlwaysUIEnabled = value;
			DebugSection.SetActive(AlwaysUIEnabled);
		}
	}
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
			_DebugSectionScript.CurrentUI.text = _DebugSectionScript.defaultCurrentUIString + _UIState.ToString();
		}
	}
	private UI UIState = UI.Initial;

	[SerializeField]
	private List<GameObject> PanelList = new List<GameObject>();

	public GameObject DebugSection;
	private DebugPanel _DebugSectionScript;

	private void Start()
	{
		_DebugSectionScript = DebugSection.GetComponent<DebugPanel>();
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
