using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UI { Initial = 1, Main, Data, Setting };
public class UIManager : MonoBehaviour
{
	private bool DebugUIEnabled;
	public bool _DebugUIEnabled
	{
		get
		{
			return DebugUIEnabled;
		}
		set
		{
			DebugUIEnabled = value;
			DebugSection.SetActive(DebugUIEnabled);
		}
	}
	private UI UIState = UI.Initial;
	public UI _UIState
	{
		get
		{
			return UIState;
		}
		set
		{
			//변경 전 UIPanel Off
			for (int i = 1; i < PanelList.Count; i++)
			{
				DisablePanel(PanelList[i]);
			}

			UIState = value;

			//변경 후 UIPanel On
			EnablePanel(PanelList[(int)UIState]);
			_DebugSectionScript.CurrentUI.text = _DebugSectionScript.defaultCurrentUIString + _UIState.ToString();
		}
	}


	[SerializeField]
	private List<GameObject> PanelList = new List<GameObject>();
	public GameObject DebugSection;

	private DebugPanel _DebugSectionScript;

	private void Awake()
	{
		_DebugSectionScript = DebugSection.GetComponent<DebugPanel>();
	}

	private void Start()
	{
		_UIState = UI.Initial;
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
