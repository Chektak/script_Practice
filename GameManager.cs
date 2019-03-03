using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public CameraControl mainCamera;
    public Player player;
    public GUIControl mainCanvas;

    private void Awake()
    {
        mainCamera.enabled = false;
        player.enabled = false;
    }
}
