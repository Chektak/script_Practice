using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPanel_Story : MonoBehaviour {
    public Text text_countDown;
    float visibleTime = 3;

      void OnEnable()
    {
        visibleTime = 3;
    }

    private void Update()
    {
        visibleTime -= Time.deltaTime;
        text_countDown.text = Mathf.Ceil(visibleTime).ToString();
        if (visibleTime <= 0f)
            gameObject.SetActive(false);
    }
}
