using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public CameraControl mainCamera;
    public Player player;
    public GUIControl mainCanvas;
    public GamePanel gamePanel;
    [Header("플레이어가 리스폰할 포지션 지정")]
    public Vector3 playerPos = new Vector3(0,160,0);

    static public bool canMainControl = true;//현재 메인화면인가?
    static public bool isGamePlaying=false;
    static public bool IsGamePlaying
    {
        get
        {
            return isGamePlaying;
        }
        set
        {
            isGamePlaying = value;
            if (value == true)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (value == false)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    private void Awake()
    {
        setControl(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGamePlaying == true)
        {
            mainCanvas.ChangePanel(3, true);
            IsGamePlaying = false;
            setControl(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isGamePlaying == false && canMainControl == false)
        {
            mainCanvas.ChangePanel(3, false);
            IsGamePlaying = true;
            setControl(true);
        }
    }

    public void GameStart()
    {
        IsGamePlaying = true;
        setControl(true);
        mainCanvas.ChangePanel(0, false);//메인화면 UI 안보이게
        mainCanvas.ChangePanel(4, true);//게임패널 온
        gamePanel.ScoreStart();
        canMainControl = false;
    }

    /// <summary>
    /// 게임플레이할떄의 UI, 카메라, 플레이어가 움직일 개체를 포함한 플레이어의 조종권
    /// </summary>
    /// <param name="setControl"></param>
    void setControl(bool setControl)
    {
        mainCamera.enabled = setControl;
        player.enabled = setControl;
    }

    /// <summary>
    /// 게임을 다시 진행시킨다.
    /// </summary>
    public void GamePause()
    {
        IsGamePlaying = true;
        setControl(true);
    }

    /// <summary>
    /// 게임을 재시작한다. 얻은 점수는 기록되지 않는다.
    /// </summary>
    public void GameReStart()
    {
        IsGamePlaying = true;
        setControl(true);
        gamePanel.ScoreReset();
        player.transform.position = playerPos;
    }

    /// <summary>
    /// 게임 플레이 종료, 얻은 점수는 기록되지 않는다.
    /// </summary>
    public void GameQuit()
    {
        IsGamePlaying = false;
        setControl(false);
        gamePanel.ScoreReset();
        player.transform.position = playerPos;
        mainCanvas.ChangePanel(0, true);
        mainCanvas.ChangePanel(4, false);
        canMainControl = true;
    }

    /// <summary>
    /// 프로그램 종료.
    /// </summary>
    public void ApplicationQuit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//유니티엔진에서 종료
#else
        Application.Quit();//실제 기기에서의 종료
#endif
    }
}
