using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
    public PowerUps powerUps;
    public PlayfabManager playfabManager;
    public bool gameEnded;
    public bool gameStarted;
    public Light2D globalLight, playerFlashLight;
    public float minLightIntensity = 0.4f;

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

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(controller.transform.position.y <= -20)
        {
            playerFlashLight.intensity += Time.deltaTime * 0.1f;
            playerFlashLight.intensity = Mathf.Clamp(playerFlashLight.intensity, playerFlashLight.intensity, 0.67f);
                
            globalLight.intensity -= Time.deltaTime * 0.1f;
            globalLight.intensity = Mathf.Clamp(globalLight.intensity, minLightIntensity, 1f);
        }
        else
        {
            playerFlashLight.intensity -= Time.deltaTime * 0.1f;
            playerFlashLight.intensity = Mathf.Clamp(playerFlashLight.intensity, 0, 0.67f);

            globalLight.intensity += Time.deltaTime * 0.1f;
            globalLight.intensity = Mathf.Clamp01(globalLight.intensity);
        }
    }
}
