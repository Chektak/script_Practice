﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public GameObject target;
    public float distance = 5f;

    public float xTurnSpeed = 220.0f;
    public float yTurnSpeed = 100.0f;

    private float xPos = 0.0f;
    private float yPos = 0.0f;

    public float yPosMinLimit = -80f;
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
        Vector3 angles = transform.eulerAngles;
        yPos = angles.y;
        xPos = angles.x;
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
            Vector3 position = rotation* new Vector3(0, 0.0f, -distance) + 
                target.transform.position+ new Vector3(0.0f, 0, 0.0f);
            transform.rotation = rotation;
            target.transform.eulerAngles = new Vector3(rotation.eulerAngles.x,rotation.eulerAngles.y,0);
            transform.position = position;
        }
    }
}
