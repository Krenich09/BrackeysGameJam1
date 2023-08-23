using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    public Transform heightLine;
    public GameObject startGameCanvas, gameplayCanvas;

    public Image dashDelayCircle;
    void Start()
    {
        gameplayCanvas.SetActive(false  );
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
    
}