using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    /*Vector3 diff;
    public GameObject target;
    public float followSpeed=5;
	// Use this for initialization
	void Start () {
        diff = target.transform.position - transform.position;
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Vector3.Lerp(transform.position, target.transform.position - diff,
            Time.deltaTime * followSpeed);
	}*/
    public GameObject target;
    public float distance = 5f;

    public float xTurnSpeed = 220.0f;
    public float yTurnSpeed = 100.0f;

    private float xPos = 0.0f;
    private float yPos = 0.0f;

    public float yPosMinLimit = -20f;
    public float yPosMaxLimit = 80f;

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;
        xPos = angles.y;
        yPos = angles.x;
        Debug.Log("x : " + xPos + "," + "y : "+yPos);
    }

    private void LateUpdate()
    {
        if (target)
        {
            distance -= 0.5f * Input.mouseScrollDelta.y;

            if (distance < 0.5)
                distance=1;
            if (distance >= 10)
                distance = 10;

            xPos += Input.GetAxis("Mouse X") * xTurnSpeed * 0.02f;
            yPos -= Input.GetAxis("Mouse Y") * yTurnSpeed * 0.02f;

            yPos = ClampAngle(yPos, yPosMinLimit, yPosMaxLimit);

            Quaternion rotation = Quaternion.Euler(yPos, xPos, 0);
            Vector3 position = rotation * new Vector3(0, 0.0f, -distance) + 
                target.transform.position+ new Vector3(0.0f, 0, 0.0f);
            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
