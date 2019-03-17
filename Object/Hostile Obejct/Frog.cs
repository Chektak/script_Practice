using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour, IHealth {
    public float jumpSpeed = 10f;
    public float maxJumpingLimitY = 10f;//점프의 최대높이
    public float minjumpDelayTime = 0.2f;
    public float maxjumpDelayTime = 1.8f;
    public float spownDelayTime = 10f;//부활 소요시간
    public float cognizeDistance = 30f;//사정거리 안에 들어오면 인식한다.
    [Header("-리스폰시 어디서 부활할지 랜덤 범위")]
    public float minRandomDirX = -200f;
    public float maxRandomDirX = 200f;
    public float minRandomDirZ = -200f;
    public float maxRandomDirZ = 200f;

    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if (hp < 0)
                Die();
        }
    }
    private int hp = 80;
    private Collider coll;
    private MeshRenderer meshRenderer;
    private bool canJump = true;

    public void Hit()
    {

    }
    public void Die()
    {
        StartCoroutine(Respown());
    }
    
    void Start()
    {
        coll = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(GameManager.Instance.player.transform.position, transform.position) < cognizeDistance)
        {
            Debug.Log(Vector3.Distance(GameManager.Instance.player.transform.position, transform.position).ToString());
            //플레이어를 바라본다.
            Vector3 playerPos = GameManager.Instance.player.transform.position;
            Quaternion rotation = Quaternion.LookRotation(playerPos, Vector3.up);
            transform.rotation = rotation;
            if (canJump == true)
                StartCoroutine(Jump((playerPos - transform.position).normalized));
        }
        else
        {
            if (canJump == true)
                StartCoroutine(Jump(new Vector3(Random.Range(-5,5), 0, Random.Range(-5, 5))));
        }
    }

    IEnumerator Jump(Vector3 dir)
    {
        canJump = false;
        float jumpingMaxY = maxJumpingLimitY;
        float jumpingDelayTime = Random.Range(minjumpDelayTime,maxjumpDelayTime);
        while (jumpingMaxY > 0)
        {
            transform.position += new Vector3(dir.x, jumpingMaxY, dir.z)*jumpSpeed*Time.deltaTime;
            jumpingMaxY -= jumpSpeed*Time.deltaTime;
            yield return null;
        }
        jumpingMaxY = maxJumpingLimitY;
        while (jumpingMaxY > 0)
        {
            transform.position -= new Vector3(-dir.x, maxJumpingLimitY-jumpingMaxY, -dir.z) * jumpSpeed*Time.deltaTime;
            jumpingMaxY -= jumpSpeed * Time.deltaTime;
            yield return null;
        }
        while (jumpingDelayTime > 0)
        {
            jumpingDelayTime -= Time.deltaTime;
            yield return null;
        }
        canJump = true;
        if (Vector3.Distance(GameManager.Instance.player.transform.position, transform.position) > 2000)//개구리가 게임 밖으로 나갔을 때
            ResetPosition();
        yield break;
    }

    IEnumerator Respown()
    {
        float spownDelay = spownDelayTime;
        meshRenderer.enabled = false;
        coll.enabled = false;
        while (spownDelay > 0)
        {
            spownDelay -= Time.deltaTime;
            yield return null;
        }
        ResetPosition();
        meshRenderer.enabled = true;
        coll.enabled = true;
        yield break;
    }
    public void ResetPosition()
    {
        transform.position = new Vector3(Random.Range(minRandomDirX, maxRandomDirX), 3, Random.Range(minRandomDirZ, maxRandomDirZ));
    }
}
