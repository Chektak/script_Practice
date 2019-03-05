using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 5f;
    public float jumpForce = 100;
    public Vector3 resetRotateValue = new Vector3();

    float zAxisforce = 0;
    float xAxisforce = 0;
    
    // Update is called once per frame
    void Update () {
        zAxisforce = 0;
        xAxisforce = 0;

        zAxisforce = Input.GetAxis("Vertical");
        xAxisforce= Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed = speed * 2;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed = speed/2;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0));
        }
        transform.Translate(new Vector3(xAxisforce, 0, zAxisforce) * speed * Time.deltaTime);
    }
}
