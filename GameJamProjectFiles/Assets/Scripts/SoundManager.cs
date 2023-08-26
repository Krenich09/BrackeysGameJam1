using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{   
    public static SoundManager instance;

    public AudioSource sourcePrefab;
    public AudioClip musicClip;
    public AudioSource musicSource;

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


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        playMusic();

        currentVolumeMusic = PlayerPrefs.GetFloat("music", 1);
        currentVolumeSFX = PlayerPrefs.GetFloat("sfx", 1);

        musicSource.volume = currentVolumeMusic;

    }





    void playMusic()
    {
        musicSource.volume = currentVolumeMusic;
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play(); 
    }

    public void playSFX(float volume, AudioClip clip)
    {
        AudioSource source = Instantiate(sourcePrefab, transform);

        source.volume = volume / currentVolumeSFX;
        source.clip = clip;

        source.Play();

        Destroy(source, clip.length);
    }
}
