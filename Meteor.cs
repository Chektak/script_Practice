using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {
    public Meteor_Fire meteor_Fire;
    public Meteor_Stone meteor_Stone;

    [Header("운석이 떨어질 범위 최소값 X")]
    public float x_MinMeteorReach = -300;
    [Header("운석이 떨어질 범위 최대값 X")]
    public float x_ManMeteorReach = 300;
    [Header("운석이 떨어질 범위 최소값 Z")]
    public float z_MinMeteorReach = -300;
    [Header("운석이 떨어질 범위 최대값 Z")]
    public float z_MaxMeteorReach = 300;
    [Header("운석이 어느 높이에서 떨어질 것인가?")]
    public float y_MeteorPos = 360;
    public float speed = 40f;
    public float resetTime = 5f;
    private Vector3 Pos_meteorStarting;
    private float posY_Control;

    private void OnEnable()
    {
        //현재 오브젝트를 랜덤한 위치로 이동시킨다.
        float RandomPos_x = Random.Range(x_MinMeteorReach, x_ManMeteorReach);
        float RandomPos_z = Random.Range(z_MinMeteorReach, z_MaxMeteorReach);

        Pos_meteorStarting = new Vector3(RandomPos_x, y_MeteorPos, RandomPos_z);
        transform.position = Pos_meteorStarting;
    }

    void LateUpdate () {
        float RandomDir_x = Random.Range(-40, 40);
        float RandomDir_z = Random.Range(-40, 40);
        transform.position += new Vector3(RandomDir_x, -speed, RandomDir_z) *Time.deltaTime;

	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            StartCoroutine(ResetPosition(resetTime));

float playerPosX=Mathf.Abs(GameManager.Instance.player.transform.position.x);
float playerPosY=Mathf.Abs(GameManager.Instance.player.transform.position.y);
float playerPosZ=Mathf.Abs(GameManager.Instance.player.transform.position.z);

float thisPosX=Mathf.Abs(transform.position.x);
float thisPosY=Mathf.Abs(transform.position.y);
float thisPosZ=Mathf.Abs(transform.position.z);

float thisScaleXYZSquare=transform.localscale.x^2+transform.localscale.y^2;
+transform.localscale.z^2;

if((thisPosX+thisPosY+thisPosZ)-(playerPosX+playerPosY+playerPosZ)
<=thisScaleXYZAddtion^2/9){
GameManager.Instance.maincamera.Shake();
}
        }
    }

    IEnumerator ResetPosition(float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        OnEnable();
        yield break;
    }
}
