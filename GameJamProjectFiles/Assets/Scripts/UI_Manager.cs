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
    public Image shieldVisualImage;
    public GameObject ShildObject;


    [Header("Sounds UI")]
    public Slider sfxSlider;
    public Slider musicSlider;

    void Start()
    {
        gameplayCanvas.SetActive(false);
        startGameCanvas.SetActive(true);
        instance = this;

        SoundManager.instance.musicSource.volume = SoundManager.instance.currentVolumeMusic;
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


            ShildObject.SetActive(GameManager.instance.powerUps.shieldOn);
            if(ShildObject.activeSelf)
            {
                ShildObject.transform.position = GameManager.instance.controller.transform.position;
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


    public void setVolumeSFX(float vol)
    {
        SoundManager.instance.currentVolumeSFX = vol;
        PlayerPrefs.SetFloat("sfx", SoundManager.instance.currentVolumeSFX);
    }
    public void GetSoundSliders()
    {
        sfxSlider.value = SoundManager.instance.currentVolumeSFX;
        musicSlider.value = SoundManager.instance.currentVolumeMusic;
    }

    public void playEngineSound()
    {
        AudioSource source = Instantiate(SoundManager.instance.sourcePrefab, transform);
        source.volume = 0f;
        source.clip = SoundManager.instance.engineSoundClip;
        source.loop = true;
        source.Play();
        SoundManager.instance.engineSource = source;
    }
    public void setVolumeMusic(float vol)
    {
        SoundManager.instance.currentVolumeMusic = vol;
        PlayerPrefs.SetFloat("music", SoundManager.instance.currentVolumeMusic);
        SoundManager.instance.musicSource.volume = musicSlider.value;
    }



    public void startGameBtnClick()
    {
        playEngineSound();
        AudioSource source = Instantiate(SoundManager.instance.sourcePrefab, transform);
        source.volume = 0.6f * SoundManager.instance.currentVolumeSFX;
        source.clip = SoundManager.instance.underwaterClip;
        source.loop = true;
        source.Play();

        GameManager.instance.gameStarted = true;
        SoundManager.instance.musicSource.volume /= 3f;
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
