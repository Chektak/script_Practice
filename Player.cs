using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 30f;
    public float jumpForce = 100;
    [HideInInspector]
    public IBuff[] deBuff;
    [HideInInspector]
    public IBuff[] gainAbility;//얻은 능력

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
isDie=true
                GameManager.instance.GameOver();
            }
        }
    }
    [HideInInspector]
    public int hp=100;

    public float dashTime = 0.2f;
    public float dashSpeed = 4f;
    public float maxDashCoolTimeLimit = 1f;
    public float NowDashCoolTime
    {
        get
        {
            return nowDashCoolTime;
        }
        set
        {
            nowDashCoolTime = value;
            
            if (nowDashCoolTime > maxDashCoolTimeLimit)
                nowDashCoolTime = maxDashCoolTimeLimit;
        }
    }
    [HideInInspector]
    public float nowDashCoolTime = 1f;
    [HideInInspector]
    public bool isDie=false;
    [HideInInspector]
    public bool canDash = true;

    float zAxisforce = 0;
    float xAxisforce = 0;
    private void Start()
    {
        GameManager.Instance.gamePanel.HpBarUpdate(hp);
    }
    // Update is called once per frame
    void FixedUpdate () {
        if (isDie == true)
            return;
        zAxisforce = 0;
        xAxisforce = 0;

        zAxisforce = Input.GetAxis("Vertical");
        xAxisforce= Input.GetAxis("Horizontal");

        if (Input.GetKeyUp(KeyCode.LeftShift) && canDash == true)
        {
            canDash = false;
            StartCoroutine(Dash());
        }
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
        float nowDashTime = dashTime;
        while (NowDashCoolTime > 0)
        {
            GameManager.Instance.gamePanel.CoolBarUpdate(nowDashCoolTime);
            if (nowDashTime > 0)
            {
                nowDashTime -= Time.deltaTime;
                transform.Translate(Vector3.forward * dashSpeed);
            }
            NowDashCoolTime -= Time.deltaTime;
            Debug.Log(NowDashCoolTime);
            yield return null;
        }
        NowDashCoolTime = maxDashCoolTimeLimit;
        canDash = true;
        
        yield break;
    }
    
}
