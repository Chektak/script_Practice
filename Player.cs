using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 30f;
    public float jumpForce = 100;

    float zAxisforce = 0;
    float xAxisforce = 0;

    bool canDash = true;

    // Update is called once per frame
    void Update () {
        zAxisforce = 0;
        xAxisforce = 0;

        zAxisforce = Input.GetAxis("Vertical");
        xAxisforce= Input.GetAxis("Horizontal");

        if (Input.GetKeyUp(KeyCode.LeftShift) && canDash == true)
            StartCoroutine(Dash());
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("!!!23");
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0));
        }
        transform.Translate(new Vector3(xAxisforce, 0, zAxisforce) * speed * Time.deltaTime);
        if (!(Input.GetKey(KeyCode.LeftAlt)))
            transform.rotation = GameManager.Instance.mainCamera.transform.rotation;
    }

    IEnumerator Dash()
    {
        canDash = false;
        const float dashTime = 0.2f;//0.25초에 걸쳐 이동
        float nowDashTime = 0;//현재 이동하고있는 시간
        while (nowDashTime<dashTime) {
            nowDashTime += Time.deltaTime;
            yield return null;
            transform.Translate(Vector3.forward * 4f);
            }
        yield return new WaitForSeconds(0.5f);
        canDash = true;
        yield break;
    }
}
