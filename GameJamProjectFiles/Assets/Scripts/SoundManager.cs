using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{   
    public static SoundManager instance;

    public AudioSource sourcePrefab;
    public AudioClip musicClip, underwaterClip, engineSoundClip, explosionClip, dashClip;
    public AudioClip[] dropClips;
    public AudioSource musicSource;
    public AudioSource engineSource;

    public float currentVolumeSFX, currentVolumeMusic;

    
    void Awake()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(gameObject); 
        } 
        else 
        { 
            instance = this; 
        }
    }

    public void playSatisfyingClip()
    {
        int randomClip = (int) Random.Range(0, dropClips.Length);
        playSFX(0.6f, dropClips[randomClip]);
    }
    public void playExplosion()
    {
        playSFX(0.6f, explosionClip);
    }
    public void playDash()
    {
        playSFX(0.6f, dashClip);
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        playMusic();

        currentVolumeMusic = PlayerPrefs.GetFloat("music", 0.5f);
        currentVolumeSFX = PlayerPrefs.GetFloat("sfx", 0.75f);

        musicSource.volume = currentVolumeMusic;

    }


    void Update()
    {
        if(engineSource)
        {
            if(Input.GetKey(KeyCode.W) && GameManager.instance.controller.canMove && !GameManager.instance.controller.isFacingWall)
            {
                engineSource.volume += Time.deltaTime;
                engineSource.volume = Mathf.Clamp(engineSource.volume, 0, 0.15f *currentVolumeSFX);
            }
            else
            {
                engineSource.volume -= Time.deltaTime * 0.3f;
                engineSource.volume = Mathf.Clamp(engineSource.volume, 0, 0.15f * currentVolumeSFX);
            }
        }
    }


    void playMusic()
    {
        musicSource.volume = currentVolumeMusic;
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play(); 
    }

    public void playSFX(float volume, AudioClip clip, bool isLooping = false)
    {
        AudioSource source = Instantiate(sourcePrefab, transform);

        source.volume = volume * currentVolumeSFX;
        source.clip = clip;

        source.Play();

        if(isLooping)
        {
            source.loop = true;
        }
        else
        {
            Destroy(source.gameObject, clip.length);
        }
    }
}
