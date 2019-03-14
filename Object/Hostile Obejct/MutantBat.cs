using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantBat : MonoBehaviour, IHealth {
    [Header("이 박쥐가 스폰될 동굴")]
    public GameObject spown_Cave;
    public float spownDelayTime = 10f;
    public float speed = 33f;
    public float flappingPosY = 6f;
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
    private int flappingStack = 0;

    void Start()
    {
        coll = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
        
    }

    public void Hit()
    {

    }
    public void Die()
    {
        StartCoroutine(ReSpown());
    }

    void FixedUpdate () {
        Vector3 relativePos = GameManager.Instance.player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        transform.rotation = rotation;
        transform.Translate(Vector3.forward * speed*Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,
            transform.position.y + flappingPosY, transform.position.z), Time.deltaTime);
        flappingStack++;
        if (flappingStack >= 24) {
            flappingPosY *= -1;
            flappingStack = 0;
        }
    }

    IEnumerator ReSpown()
    {
        float spownDelay=spownDelayTime;
        meshRenderer.enabled = false;
        coll.enabled = false;
        while (spownDelay > 0)
        {
            spownDelay -= Time.deltaTime;
            yield return null;
        }
        transform.position=spown_Cave.transform.position;
        meshRenderer.enabled = true;
        coll.enabled = true;
        yield break;
    }
}
