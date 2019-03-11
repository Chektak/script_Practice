using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {
    public Text text_aliveTime;
    public Image image_Story;
    public GameObject panel_gameOver;
    
    [Header("라운드, 카운트다운 등의 알림을 표시할 Text")]
    public Text text_Notice;
    [Header("체력과 대쉬기능의 쿨타임을 표시하는 슬라이더")]
    public Slider slider_HpBar;
    public Slider slider_DashCoolTimeBar;

    private int aliveMinute=0;
    [SerializeField]
    private float aliveSecond = 0;
    public float countTime = 3;

    private void Start()
    {
        ScoreReset();
        text_aliveTime.text = aliveMinute + ":" + aliveSecond;
    }

    public void ScoreReset()
    {
        aliveSecond = 0;
        aliveMinute = 0;
    }

    /// <summary>
    /// 생존시간을 기록한다. 생존시간 1분마다 라운드가 하나씩 넘어간다.
    /// </summary>
    public void ScoreStart()
    {
        StartCoroutine(scoreCalculator());
    }

    IEnumerator scoreCalculator()
    {
        do{
            if (GameManager.Instance.IsGamePlaying == true)
            {
                aliveSecond += Time.deltaTime;
                text_aliveTime.text = aliveMinute + ":" + Mathf.Ceil(aliveSecond).ToString();
                if (aliveSecond >= 60)
                {
                    aliveSecond = aliveSecond % 60;
                    aliveMinute++;
                    GameManager.Instance.nowRound = aliveMinute+1;
                    GameManager.Instance.SetRoundJump(GameManager.Instance.nowRound);
                }
            }
            yield return null;
        } while (GameManager.Instance.isMainScreen == false);
        yield break;
    }

    /// <summary>
    /// 인잣값 duration에 준 시간 후에 공지텍스트를 초기화합니다.
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator ResetForNoticeDuration(float duration)
    {
        float time = 0;
        while (time<duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        text_Notice.text = "";
        yield break;
    }
    
    /// <summary>
    /// 메인화면에서 GameStart버튼을 눌렀을 때 실행되는 이벤트이다.
    /// </summary>
    public void CountDown()
    {
        StartCoroutine(countDown());
    }
    /// <summary>
    /// 카운트다운이 끝나고 GameManager.GameStart() 함수가 실행된다.
    /// </summary>
    /// <returns></returns>
    IEnumerator countDown()
    {
        countTime = 3;
        while (countTime > 0)
        {
            countTime -= Time.deltaTime;
            text_Notice.text = Mathf.Ceil(countTime).ToString();

            yield return null;
        }
        image_Story.gameObject.SetActive(false);
        GameManager.Instance.GameStart();
        ChangeNoticeText("Round 1", 2f);
        yield break;
    }
    public void ChangeNoticeText(string text, float duration)
    {
        text_Notice.text = text;
        StartCoroutine(ResetForNoticeDuration(duration));
    }

    /// <summary>
    /// 게임패널의 hp바를 업데이트한다.
    /// </summary>
    /// <param name="hp"></param>
    public void HpBarUpdate(int hp)
    {
        slider_HpBar.value = hp;
    }

    /// <summary>
    /// 게임패널의 Dash_cooltime바를 업데이트한다.
    /// </summary>
    /// <param name="cooltime"></param>
    public void CoolBarUpdate(float cooltime)
    {
        slider_DashCoolTimeBar.value = cooltime;

    }
    
}
