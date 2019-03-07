using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {
    public Text Text_aliveTime;
    public Image Image_Story;

    [Header("라운드, 카운트다운 등의 알림을 표시할 Text")]
    public Text Text_Notice;

    private int aliveMinute=0;
    private float AliveSecond
    {
        get
        {
            return aliveSecond;
        }
        set
        {
            aliveSecond = value;
            if (aliveSecond>=60)
            {
                aliveSecond = aliveSecond%60;
                aliveMinute++;
            }
        }
    }
    private float aliveSecond = 0;
    private float countTime = 3;

    private void Start()
    {
        aliveMinute = 0;
        AliveSecond = 0;
        Text_aliveTime.text = aliveMinute + ":" + aliveSecond;
    }

    public void ScoreStart()
    {
        StartCoroutine(scoreCalculator());
    }

    IEnumerator scoreCalculator()
    {
        do
        {
            if (GameManager.Instance.isGamePlaying == true)
            {
                AliveSecond += Time.deltaTime;
                Text_aliveTime.text = aliveMinute+":"+Mathf.Ceil(AliveSecond).ToString();
            }
            
            yield return null;
        }
        while (GameManager.Instance.canMainControl == false);

        yield break;
    }

    public void ScoreReset()
    {
        AliveSecond = 0;
        aliveMinute = 0;
    }

    public double ReturnAliveTime()
    {
        return aliveMinute * 60 + aliveSecond;
    }
    
    public void CountDown()
    {
        StartCoroutine(Count());
    }
    IEnumerator Count()
    {
        while (countTime > 0)
        {
            countTime -= Time.deltaTime;
            Text_Notice.text = Mathf.Ceil(countTime).ToString();

            yield return null;
        }
        Text_Notice.text = "";
        Image_Story.gameObject.SetActive(false);
        GameManager.Instance.GameStart();
        yield break;
    }
}
