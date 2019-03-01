using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 5f;
    public Vector3 resetRotateValue = new Vector3();
    private Vector3 position = new Vector3();
    private bool isGround = true;
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("w"))
            position += new Vector3(0, 0, 1f);
        if (Input.GetKeyDown("a"))
            position= new Vector3(-1f, 0, 0);
        if (Input.GetKeyDown("d"))
            position = new Vector3(1f, 0, 0);
        if (Input.GetKeyDown("s"))
            position = new Vector3(0, 0, -1f);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            transform.rotation = Quaternion.Euler(resetRotateValue.x, resetRotateValue.y, resetRotateValue.z);
        if (Input.GetKeyDown(KeyCode.Space)&&isGround==true)
        {
            position = new Vector3(0, 5, 0);
            isGround = false;
        }
        transform.position += position * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGround = true;
    }
}
