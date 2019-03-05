using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {
    public Text Text_aliveTime;
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
            if (GameManager.isGamePlaying == true)
            {
                AliveSecond += Time.deltaTime;
                Text_aliveTime.text = aliveMinute+":"+Mathf.Ceil(AliveSecond).ToString();
            }
            
            yield return null;
        }
        while (GameManager.canMainControl == false);

        yield break;
    }

    public void ScoreReset()
    {
        AliveSecond = 0;
        aliveMinute = 0;
    }
}
