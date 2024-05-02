using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class UIChange : MonoBehaviour
{
    private int UIState = 0;
    public int _UIState
    {
        get
        {
            return UIState;
        }
        set
        {
            //변경 전 모두 Off
            for (int i = 0; i < UIList.Count; i++)
            {
                DisableUI(UIList[i]);
            }

            UIState = value;

            //변경 후 On
            EnableUI(UIList[UIState]);
        }
    }

    [SerializeField]
    private List<GameObject> UIList = new List<GameObject>();

    public virtual void Awake()
    {
        _UIState = 0;
    }

    public void DisableUI(GameObject ui)
    {
        ui.SetActive(false);
    }
    public void EnableUI(GameObject ui)
    {
        ui.SetActive(true);
    }
    public void ChangeUI(int index)
    {
        _UIState = index;
    }
}
