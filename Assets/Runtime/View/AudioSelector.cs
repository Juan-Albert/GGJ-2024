using System;
using UnityEngine;

public class AudioSelector : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void Play(string soundId)
    {
        if(soundId == "Sound")
            audioSource.Play();
    }
}