using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    public Transform heightLine;
    public GameObject startGameCanvas, gameplayCanvas, endGameCanvas, pauseCanvas;

    public Image dashDelayCircle;
    public TMP_Text depthTextGameEnd;
    public bool isPaused;

    [Header("Start Menu")]
    public TMP_InputField newlyNameInput;
    public TMP_Text changeNameErrorTxt;
    public GameObject newlyCreatedPanel;

    [Header("Leaderboard")]
    public Transform leaderBoardParent;
    public GameObject leaderBoardPrefab;


    [Header("Settings Menu")]
    public TMP_InputField changeNameSettingsInput;
    public TMP_Text changeNamesettingsError;

    [Header("PowerUps Visual")]
    public GameObject healthPowerUpPartical;
    public GameObject oxygenPowerUpPartical;
    public GameObject shieldPowerUpPartical;
    void Start()
    {
        gameplayCanvas.SetActive(false);
        startGameCanvas.SetActive(true);
        instance = this;
    }
    float refVelocity; // ignore this
    void Update()
    {
        if(GameManager.instance.gameStarted)
        {
            heightLine.gameObject.SetActive(true);

            // Dash Delay Circle
            if(GameManager.instance.controller.dashDelayCurrent > 0)
            {
                dashDelayCircle.fillAmount = GameManager.instance.controller.dashDelayCurrent;
            }
            else
            {
                dashDelayCircle.fillAmount = 0;
            }
        }
        else
        {
            heightLine.gameObject.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            switchPause();
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        heightLine.position = new Vector3(GameManager.instance.controller.transform.position.x, -GameManager.instance.meter.currentHighScore, 0);
    }

    public void startGameBtnClick()
    {
        GameManager.instance.gameStarted = true;
        gameplayCanvas.SetActive(true);
        startGameCanvas.SetActive(false);
        GameManager.instance.camController.target = GameManager.instance.controller.transform;
        GameManager.instance.controller.rb.gravityScale = 0.01f;
    }
    public void changeNameSettings()
    {
        GameManager.instance.playfabManager.changeDisplayName(changeNameSettingsInput.text, true);
    }
    public void changeNameNewly()
    {
        GameManager.instance.playfabManager.changeDisplayName();
    }

    public void endGameUI()
    {
        endGameCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        depthTextGameEnd.text = Mathf.Round(GameManager.instance.meter.currentHighScore).ToString() + "m";
        Time.timeScale = 1;
        isPaused = false;
    }

    public void switchPause()
    {
        if(GameManager.instance.gameStarted == false || GameManager.instance.gameEnded == true) return;
        isPaused = !isPaused;
        pauseCanvas.SetActive(isPaused);
        if(isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    } 
}
