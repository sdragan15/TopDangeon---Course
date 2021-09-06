using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicClass : MonoBehaviour
{
    public AudioSource backgroundMusic;


    private void Awake()
    {

        backgroundMusic = GetComponent<AudioSource>();
       
    }

    public void PlayMusic()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    public void StopMusic()
    {
        backgroundMusic.Stop();
    }


}
