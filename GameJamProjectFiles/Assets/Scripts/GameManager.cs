using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Systems")]
    public PlayerController controller;
    public DepthMeter meter;
    public CameraController camController;
    public RandomObsticalSpawn randomObsticalSpawn;
    public HealthSystem healthSystem;
    public PlayfabManager playfabManager;
    public bool gameEnded;
    public bool gameStarted;

    void Start()
    {
        instance = this;
        Time.timeScale = 1;
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

    public void EndGame()
    {
        Debug.Log("Game Ended");
        gameEnded = true;

        UI_Manager.instance.endGameUI();
        playfabManager.sendLeaderboard(Mathf.RoundToInt(meter.currentHighScore));
    }


    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
