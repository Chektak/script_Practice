using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    static public CameraControl mainCamera;
    static public Player player;
    public GUIControl mainCanvas;

    static public bool isGamePlaying = false;
    private void Awake()
    {
        ChangeControl(false); 
    }

    /// <summary>
    /// GUIControl에서 게임 일시정지와 Start버튼에서 사용한다.
    /// </summary>
    static public void GamePlay()
    {
        isGamePlaying = true;
    }

    /// <summary>
    /// 게임플레이를 가능하게 할 지(카메라와 플레이어 이동) 설정한다.
    /// </summary>
    /// <param name="canPlay"></param>
    static public void ChangeControl(bool canPlay) {
        mainCamera.enabled = canPlay;
        player.enabled = canPlay;
    }
}
