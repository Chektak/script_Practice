using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    public int touchingPlayerSetHP = 20;
    [Header("랜덤스폰 포지션 최소값 X")]
    public float x_MinSpownReach = -300;
    [Header("랜덤스폰 포지션 최대값 X")]
    public float x_MaxSpownrReach = 300;
    [Header("랜덤스폰 포지션 최소값 Z")]
    public float z_MinSpownReach = -300;
    [Header("랜덤스폰 포지션 최대값 Z")]
    public float z_MaxSpownReach = 300;

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float RandomPos_x = Random.Range(x_MinSpownReach, x_MaxSpownrReach);
            float RandomPos_z = Random.Range(z_MinSpownReach, z_MaxSpownReach);

            Vector3 Pos_RandomSpown = new Vector3(RandomPos_x, 10, RandomPos_z);

            collision.gameObject.transform.position = Pos_RandomSpown;
            GameManager.Instance.player.Hp = touchingPlayerSetHP;
            Debug.Log(GameManager.Instance.player.Hp);
        }
    }

}
