using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIControl : MonoBehaviour {
    public GameObject[] panels;
    public int openPanelNumber=0;

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.isGamePlaying == false)
        {
            ChangeMainPanel();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameManager.isGamePlaying == true)
        {
            ChangePanel(3);
            GameManager.ChangeControl(false);
        }
    }
	
	/// <summary>
    /// 패널을 바꾼다. 0:메인화면 1:옵션 2:크레딧 3:일시정지화면
    /// </summary>
    /// <param name="number"></param>
	public void ChangePanel (int number) {
        panels[openPanelNumber].SetActive(false);
        openPanelNumber = number;
        panels[openPanelNumber].SetActive(true);
	}
    
    /// <summary>
    /// 메인화면으로 이동한다.
    /// </summary>
    public void ChangeMainPanel() {
        panels[openPanelNumber].SetActive(false);
        openPanelNumber = 0;
        panels[openPanelNumber].SetActive(true);
    }
}
