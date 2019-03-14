using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour {
    public GameObject target;
    public float distance = 10f;

    public float xTurnSpeed = 220.0f;
    public float yTurnSpeed = 100.0f;

    private float xPos = 0.0f;
    private float yPos = 0.0f;

    public float yPosMinLimit = -80f;
    public float yPosMaxLimit = 80f;

    [Header("카메라 진동 이벤트")]
    public float shakeTime = 3.0f;
    public float shakeAmount = 3.0f;
    public float shakeSpeed = 10.0f;

    private bool isShaking = false;
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
                distance = 1;
            if (distance >= 30)
                distance = 30;

            xPos += Input.GetAxis("Mouse X") * xTurnSpeed * 0.02f;
            yPos -= Input.GetAxis("Mouse Y") * yTurnSpeed * 0.02f;

            yPos = ClampAngle(yPos, yPosMinLimit, yPosMaxLimit);

            Quaternion rotation = Quaternion.Euler(yPos, xPos, 0);

            Vector3 position = rotation * new Vector3(0, 0.0f, -distance) +
                target.transform.position;
            if (position.y < 2)//카메라가 땅 아래로 가지 않도록 고정
                position = new Vector3(position.x, 2, position.z);

            Vector3 randomPos = position;
            if (isShaking == true)
            {
                randomPos = position + Random.insideUnitSphere * shakeAmount;
                //Random.insideUnitSphere : 반경 1을 갖는 구 안의 임의의 지점을 반환한다.
            }

            transform.rotation = rotation;
            transform.position = Vector3.Lerp(position, randomPos, Time.deltaTime * shakeSpeed);
        }
    }

    /// <summary>
    /// X축 감도 설정
    /// </summary>
    /// <param name="slider"></param>
    public void ChangeXAxisSensitivity(Slider slider)
    {
        xTurnSpeed = 440 * slider.value;
    }

    /// <summary>
    /// Y축 감도 설정
    /// </summary>
    /// <param name="slider"></param>
    public void ChangeYAxisSensitivity(Slider slider)
    {
        yTurnSpeed = 440 * slider.value;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera()
    {
        isShaking = true;
        float duration = shakeTime;
        while (duration>0)
        {
            //LateUpdate에서 실행된다.
            duration -= Time.deltaTime;
            yield return null;
        }
        isShaking = false;
        yield break;
    }

    /// <summary>
    /// 플레이어가 피격됬을떄 호출되는 함수로, 화면을 잠깐 빨갛게 한다.
    /// </summary>
    public void Hitting()
    {
        StartCoroutine(HittingCamera());
    }

    IEnumerator HittingCamera()
    {
        GameManager.Instance.gamePanel.panel_Hitting.SetActive(true);
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        GameManager.Instance.gamePanel.panel_Hitting.SetActive(false);
        yield break;
    }

    /// <summary>
    /// 플레이어의 체력이 늘어났을 때 호출되는 함수로, 화면을 잠깐 초록색으로 한다.
    /// </summary>
    public void Healing()
    {
        StartCoroutine(HealingCamera());
    }

    IEnumerator HealingCamera()
    {
        GameManager.Instance.gamePanel.panel_Healing.SetActive(true);
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        GameManager.Instance.gamePanel.panel_Healing.SetActive(false);
        yield break;
    }
}
