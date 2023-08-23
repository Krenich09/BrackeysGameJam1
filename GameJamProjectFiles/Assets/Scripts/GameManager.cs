using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Systems")]
    public PlayerController controller;
    public DepthMeter meter;
    public CameraController camController;
    public RandomObsticalSpawn randomObsticalSpawn;
    public HealthSystem healthSystem;

    public bool gameStarted;

    void Start()
    {
        instance = this;
    }



    public bool checkIfFacingDown(Transform target, float downwardThreshold)
    {
        float zRotation = target.localRotation.eulerAngles.z;

        if (Mathf.Abs(Mathf.DeltaAngle(zRotation, 180f)) <= downwardThreshold || Mathf.Abs(Mathf.DeltaAngle(zRotation, -180f)) <= downwardThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
