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
            if (isDie == true)//이미 죽어있다면 체력을 계산하지 않는다.
                return;
            if (hp > value)
                GameManager.Instance.mainCamera.Hitting();//체력이 줄어들면 화면이 빨개진다.
            if (hp < value)
                GameManager.Instance.mainCamera.Healing();//체력이 늘어나면 화면이 초록색으로 된다.
            hp = value;
            GameManager.Instance.gamePanel.HpBarUpdate(hp);
            if (hp > maxHpLimit)
                hp = maxHpLimit;
            else if (hp < 0)
            {
                isDie = true;
                GameManager.instance.GameOver();
            }
        }
    }
    [HideInInspector]
    public int hp=100;
    public float maxDashCoolTimeLimit = 0.2f;
    public float nowdashCoolTime = 0.2f;
    public float dashDuration = 0.2f;
    public float dashSpeed = 4f;
    public float energyRecoverySpeed = 1;//기력 재생 빠르기
    public int maxEnergyLimit = 100;
    public int Energy
    {
        get
        {
            return energy;
        }
        set
        {
            energy = value;
            GameManager.Instance.gamePanel.CoolBarUpdate(energy);
            if (energy > maxEnergyLimit)
                energy = maxEnergyLimit;
        }
    }
    private int energy = 100;

    [HideInInspector]
    public bool isDie=false;
    [HideInInspector]
    public bool canDash = true;

    private void Start()
    {
        GameManager.Instance.gamePanel.HpBarUpdate(hp);
    }
    float zAxisforce = 0;
    float xAxisforce = 0;
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
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0));
        }
        if (!(Input.GetKey(KeyCode.LeftAlt)))
            transform.rotation = GameManager.Instance.mainCamera.transform.rotation;
        transform.Translate(new Vector3(xAxisforce, 0, zAxisforce) * speed * Time.deltaTime);

        Energy += (int)(energyRecoverySpeed*Time.deltaTime);
    }

    IEnumerator Dash()
    {
        canDash = false;
        float nowDashTime = dashDuration;
        while (Energy > 0&& nowDashTime>0)
        {
            nowDashTime -= Time.deltaTime;
            transform.Translate(Vector3.forward * dashSpeed);
            Energy -= 2;
            yield return null;
        }
        while (nowdashCoolTime > 0)
        {
            nowdashCoolTime -= Time.deltaTime;
            yield return null;
        }
        nowdashCoolTime = maxDashCoolTimeLimit;
        canDash = true;
        
        yield break;
    }
    
}
