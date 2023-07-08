using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    //public AudioSource backgroundMusicSource;
    public AudioSource runSoundSource;

    public AudioSource audioSource;
    public AudioClip backgroundMusic;
    public AudioClip jumpSound;
    public AudioClip runSound;


    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayRunSound()
    {
        runSoundSource.clip = runSound;
        runSoundSource.Play();
        //audioSource.PlayOneShot(runSound);
    }

    public void StopCurrentSound()
    {
        runSoundSource.Stop();
    }
}

