using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.Singleton;

public class UIManager : UIChange
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

	public GameObject DebugSection;
	private DebugPanel _DebugSectionScript;

	public override void Awake()
	{
		base.Awake();
		_DebugSectionScript = DebugSection.GetComponent<DebugPanel>();
	}

}
