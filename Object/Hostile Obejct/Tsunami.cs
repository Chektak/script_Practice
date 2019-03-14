using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsunami : MonoBehaviour {
    public Tsunami_RunningWater runningWater;
    public Tsunami_StandingWater[] standingWaters;

    [Header("해일 랜덤 생성포인트, 어디를 바라보고 올지 할당한다.")]
    public Vector3[] tsunamiPoint = new Vector3[4] { new Vector3(0, 140, 390),
        new Vector3(0, 140, -390), new Vector3(390, 140, 0), new Vector3(390, 140, 0)};
    public Vector3[] tsunamiRotate = new Vector3[4] { new Vector3(0, -180, 0),
        new Vector3(0,0,0), new Vector3(0, -90, 0), new Vector3(0, 90, 0) };

    private int randomIndex = 0;

    void Start () {
        Reset_RunningWater();
        StartCoroutine(Resetting_RunningWater());
	}

    private void Reset_RunningWater()
    {
        randomIndex = Random.Range(0, 3);
        runningWater.transform.position = tsunamiPoint[randomIndex];
        runningWater.transform.rotation = Quaternion.Euler(tsunamiRotate[randomIndex]);
        runningWater.gameObject.SetActive(true);
    }

    IEnumerator Resetting_RunningWater()
    {
        while (runningWater.gameObject.activeSelf==true)
        {
            yield return null;
        }
        Reset_RunningWater();
        yield break;
    }
}
