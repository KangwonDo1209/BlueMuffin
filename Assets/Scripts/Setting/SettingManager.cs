using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.Singleton;

public enum SettingPanel { Connection = 1, Alarm, Statistic, Others };
public class SettingManager : Singleton<UIManager>
{
	[SerializeField]
	private List<GameObject> PanelList = new List<GameObject>();
	private SettingPanel PanelState = SettingPanel.Connection;
	public SettingPanel _PanelState
	{
		get
		{
			return PanelState;
		}
		set
		{
			for (int i = 1; i < PageList.Count; i++)
			{
				DisablePanel(PageList[i]);
			}
			PanelState = value;
			EnablePanel(PageList[(int)PanelState]);
		}
	}


	[SerializeField]
	private List<GameObject> PageList = new List<GameObject>();

	public override void Awake()
	{
		base.Awake();
	}

	private void Start()
	{
		PanelState = SettingPanel.Connection;
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
		_PanelState = (SettingPanel)index;
	}




}
