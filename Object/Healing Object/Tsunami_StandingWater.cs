using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsunami_StandingWater : MonoBehaviour {
    public float scaleShrink = 0.05f;
    public float resetCycleTime = 20f;
    
    [Header("물이 생성될 범위 최소값 X")]
    public float x_MinWaterReach = -230;
    [Header("물이 생성될 범위 최대값 X")]
    public float x_ManWaterReach = 230;
    [Header("물이 생성될 최소값 Z")]
    public float z_MinWaterReach = -230;
    [Header("물이 생성될 최대값 Z")]
    public float z_MaxWaterReach = 230;
    [Header("물이 어느 높이에서 생성될 것인가?")]
    public float y_WaterPos = 1.25f;

    private Vector3 Pos_WaterStarting;
    private Vector3 resetScale;

    private void OnEnable()
    {
        resetScale = transform.localScale;
        StartCoroutine(Resetting());
    }

    void ResetState()
    {
        float RandomPos_x = Random.Range(x_MinWaterReach, x_ManWaterReach);
        float RandomPos_z = Random.Range(z_MinWaterReach, z_MaxWaterReach);

        Pos_WaterStarting = new Vector3(RandomPos_x, y_WaterPos, RandomPos_z);
        transform.position = Pos_WaterStarting;

        transform.localScale = resetScale;
    }
    private IEnumerator Resetting()
    {
        while (gameObject.activeSelf == true) {
            float duration = resetCycleTime;
            while (duration > 0)
            {
                duration -= Time.deltaTime;
                yield return null;
            }
            ResetState();
        }
        yield break;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (transform.localScale.y > 1&& GameManager.Instance.player.Hp != GameManager.Instance.player.maxHpLimit)
            {
                transform.localScale -= new Vector3(scaleShrink, scaleShrink, scaleShrink);
                GameManager.Instance.player.Hp += 1;
            }
        }
    }
}
