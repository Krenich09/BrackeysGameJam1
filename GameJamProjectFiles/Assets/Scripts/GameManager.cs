using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController controller;
    public DepthMeter meter;
    public CameraController camController;
    public bool gameStarted;

    void Start()
    {
        instance = this;
    }
}
