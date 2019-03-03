using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIControl : MonoBehaviour {
    public GameObject[] panels;
    public int openPanelNumber=0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void ChangePanelNumber (int n) {
        openPanelNumber = n;
	}
}
