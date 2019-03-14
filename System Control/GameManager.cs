using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public CameraControl mainCamera;
    public Player player;
    public GUIControl mainCanvas;
    public GamePanel gamePanel;

    [Header("-플레이어가 스폰될 장소 지정")]
    public GameObject playerSpawn;

    [Header("-1라운드마다 하나씩 자연재해")]
    public ICalamity[] calamitys;

    public int nowRound = 1;
    public bool isMainScreen = true;//현재 메인화면인가?
    public bool IsGamePlaying
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
    private bool isGamePlaying = false;

    

    //싱글턴 패턴 ******************************************/
    static public GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    static public GameManager instance;
    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
    //게임 플레이할때 이벤트들******************************//

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGamePlaying == true && gamePanel.panel_gameOver.activeSelf==false)
        {
            {
                IsGamePlaying = false;
                setControl(false);
            }
            mainCanvas.NowOpenPanel = 3;
            mainCanvas.ChangePanel(3, true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isGamePlaying == false && mainCanvas.panels[3].activeSelf==true)
        {
            {
                IsGamePlaying = true;
                setControl(true);
            }
            mainCanvas.ChangePanel(3, false);
        }
    }

    /// <summary>
    /// 함수 실행 순서 : 메인화면에서 GameStart버튼 클릭->gamePanel.CountDown->GameManager.GameStart()->gamePanel.ScoreStart();
    /// </summary>
    public void GameStart()
    {
        {
            IsGamePlaying = true;
            setControl(true);
            player.isDie = false;
            player.Hp = player.maxHpLimit;
        }
        mainCanvas.ChangePanel(0, false);//메인화면 UI 안보이게
        isMainScreen = false;
        SetRoundJump(1);//1라운드로 설정
        gamePanel.ScoreStart();
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
    /// 해당 라운드로 이동한다. 1라운드로 이동하려면 인잣값으로 1을 넣으면 동작한다.
    /// </summary>
    /// <param name="round"></param>
    public void SetRoundJump(int round)
    {
        nowRound = round;
        gamePanel.ChangeNoticeText("Round " + round, 2f);
        if (round -1 == -1)
            gamePanel.ChangeNoticeText("(Easter Egg) 엔딩 2-타임패러독스" + round, 5f);
        else
            calamitys[round-1].CalamityStart();
    }

    public void GameOver()
    {
        {//플레이어가 죽었을때만 쓸 수 있는 능력이 있기 때문에 플레이어 스크립트를 비활성화하지 않는다.
            IsGamePlaying = false;
            mainCamera.enabled = false;
        }
        gamePanel.panel_gameOver.SetActive(true);
    }
    //UI버튼 이벤트들**************************************************************************************

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
        {//이미 발생한 자연재해들을 해제
            for (int i = 1; i < nowRound; i++)
            {
                calamitys[i].CalamityRelese();
            }
            nowRound = 1;
        }
        {//메인화면에서 처음 시작하는 느낌을 주도록 구현
            gamePanel.ScoreReset();
            IsGamePlaying = false;
            Time.timeScale = 1;
            setControl(false);
            gamePanel.CountDown();
        }
        player.transform.position = playerSpawn.transform.position+new Vector3(0, 8, 0);
    }

    /// <summary>
    /// 게임 플레이 종료, 얻은 점수는 기록되지 않는다.
    /// </summary>
    public void GameQuit()
    {
        gamePanel.ScoreReset();
        IsGamePlaying = false;
        setControl(false);
        player.transform.position = playerSpawn.transform.position + new Vector3(0, 8, 0);
        mainCanvas.ChangePanel(0, true);
        mainCanvas.ChangePanel(4, false);
        isMainScreen = true;
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
