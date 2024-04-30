using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.Singleton;

public enum UIPage { Initial = 1, Main, Data, Setting };
public class UIManager : Singleton<UIManager>
{
	private bool DebugPanelEnabled;
	public bool _DebugPanelEnabled
	{
		get
		{
			return DebugPanelEnabled;
		}
		set
		{
			DebugPanelEnabled = value;
			DebugSection.SetActive(DebugPanelEnabled);
		}
	}
	private UIPage UIState = UIPage.Initial;
	public UIPage _UIState
	{
		get
		{
			return UIState;
		}
		set
		{
			//변경 전 UIPanel Off
			for (int i = 1; i < PageList.Count; i++)
			{
				DisablePanel(PageList[i]);
			}

			UIState = value;

			//변경 후 UIPanel On
			EnablePanel(PageList[(int)UIState]);
			_DebugSectionScript.CurrentUI.text = _DebugSectionScript.defaultCurrentUIString + _UIState.ToString();
		}
	}


	[SerializeField]
	private List<GameObject> PageList = new List<GameObject>();
	public GameObject DebugSection;

	private DebugPanel _DebugSectionScript;

	public override void Awake()
	{
		base.Awake();
		_DebugSectionScript = DebugSection.GetComponent<DebugPanel>();
	}

	private void Start()
	{
		_UIState = UIPage.Initial;
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
		_UIState = (UIPage)index;
	}
}
