using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 30f;
    public float jumpForce = 100;
    public IBuff[] buffs;
    public int maxHpLimit = 100;
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            GameManager.Instance.gamePanel.HpBarUpdate(hp);
            if (hp > maxHpLimit)
                hp = maxHpLimit;
            else if (hp < 0)
            {
                isAlive = false;
                GameManager.instance.GameOver();
            }
        }
    }
    private int hp=100;

    public float maxDashCoolTimeLimit = 0.5f;
    public float DashCoolTime
    {
        get
        {
            return dashCoolTime;
        }
        set
        {
            dashCoolTime = value;
            GameManager.Instance.gamePanel.CoolBarUpdate(dashCoolTime);
            if (dashCoolTime > maxDashCoolTimeLimit)
                dashCoolTime = maxDashCoolTimeLimit;
        }
    }
    public float dashCoolTime = 0.5f;
    float zAxisforce = 0;
    float xAxisforce = 0;

    public bool isAlive = true;
    public bool canMove = true;
    bool canDash = true;

    private void Start()
    {
        GameManager.Instance.gamePanel.HpBarUpdate(hp);
    }
    // Update is called once per frame
    void Update () {
        if (canMove == false)
            return;
        zAxisforce = 0;
        xAxisforce = 0;

        zAxisforce = Input.GetAxis("Vertical");
        xAxisforce= Input.GetAxis("Horizontal");

        if (Input.GetKeyUp(KeyCode.LeftShift) && canDash == true)
            StartCoroutine(Dash());
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0));
        }
        if (!(Input.GetKey(KeyCode.LeftAlt)))
            transform.rotation = GameManager.Instance.mainCamera.transform.rotation;
        transform.Translate(new Vector3(xAxisforce, 0, zAxisforce) * speed * Time.deltaTime);
        
        if(transform.position.y<-5)
            transform.position=GameManager.Instance.playerSpawn.transform.position + new Vector3(0, 8, 0);
    }

    IEnumerator Dash()
    {
        canDash = false;
        const float dashTime = 0.2f;//0.2초에 걸쳐 이동
        
        float nowDashTime = 0;//현재 이동하고있는 시간
        while (nowDashTime<dashTime) {
            nowDashTime += Time.deltaTime;
            yield return null;
            transform.Translate(Vector3.forward * 4f);
            }
        while (dashCoolTime < 0)
        {
            dashCoolTime -= Time.deltaTime;
            yield return null;
        }
        canDash = true;
        yield break;
    }
}
