using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {

    [Header("박쥐가 스폰될 범위 최소값 X")]
    public float x_MinSpownReach = -300;
    [Header("박쥐가 스폰될 범위 최대값 X")]
    public float x_MaxSpowmReach = 300;
    [Header("박쥐가 스폰될 범위 최소값 Z")]
    public float z_MinSpownReach = -300;
    [Header("박쥐가 스폰될 범위 최대값 Z")]
    public float z_MaxSpownReach = 300;
    public float y_SpownPos = 360;
    public float speed = 40f;
    private Vector3 Pos_meteorStarting;
    private float posY_Control;

    private void OnEnable()
    {
        //현재 오브젝트를 랜덤한 위치로 이동시킨다.
        float RandomPos_x = Random.Range(x_MinSpownReach, x_MaxSpowmReach);
        float RandomPos_z = Random.Range(z_MinSpownReach, z_MaxSpownReach);

        Pos_meteorStarting = new Vector3(RandomPos_x, y_SpownPos, RandomPos_z);
        transform.position = Pos_meteorStarting;
    }

    void LateUpdate () {
        float RandomDir_x = Random.Range(-40, 40);
        float RandomDir_z = Random.Range(-40, 40);
        transform.position += new Vector3(RandomDir_x, -speed, RandomDir_z) *Time.deltaTime;

	}

}
