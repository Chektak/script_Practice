using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIControl : MonoBehaviour {
    [Header("0:메인화면 1:옵션 2:크레딧 3:일시정지 화면")]
    public GameObject[] panels;

    [SerializeField]
    private int nowOpenPanel = 0;
    private int NowOpenPanel
    {
        get
        {
            return nowOpenPanel;
        }
        set
        {
            pastOpenPanel = nowOpenPanel;
            nowOpenPanel = value;
        }
    }
    private int pastOpenPanel;//과거에 열었던 패널을 기록

    /// <summary>
    /// 패널의 활성화상태를 바꾼다. 0:메인화면 1:옵션 2:크레딧 3:일시정지화면... , true로 할지 false로 할지.
    /// </summary>
    /// <param name="number"></param>
    /// <param name="setBool"></param>
    public void ChangePanel (int number, bool setBool) {
        panels[number].SetActive(setBool);
        if (setBool == true) { 
            NowOpenPanel = number;//패널을 열고있다면 열고있는 패널로 간주
        }
	}

    public void PanelGoBack()
    {
        ChangePanel(nowOpenPanel, false);
        ChangePanel(pastOpenPanel, true);
    }
}
