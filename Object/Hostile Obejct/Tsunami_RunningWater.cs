using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsunami_RunningWater : MonoBehaviour {
    //쓰나미에 닿으면 플레이어는 대쉬를 쓸 수 없게된다.

    public float speed = 13f;
    public float endLineDistance = 800;

    private Vector3 startPos;

    private bool canControlPlayerDash = true;
    private void OnEnable()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Vector3.Distance(startPos, transform.position) > endLineDistance)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player"&& canControlPlayerDash==true)
        {
            canControlPlayerDash = false;
            StartCoroutine(PlayerCantDash());
        }
    }

    IEnumerator PlayerCantDash()
    {
        float duration = 4f;

        while (duration > 0)
        {
            duration -= Time.deltaTime;
            GameManager.Instance.player.NowDashCoolTime = 0.1f;
            yield return null;
        }
        GameManager.Instance.player.NowDashCoolTime = GameManager.Instance.player.maxDashCoolTimeLimit;
        canControlPlayerDash = true;
        yield break;
    }
}
