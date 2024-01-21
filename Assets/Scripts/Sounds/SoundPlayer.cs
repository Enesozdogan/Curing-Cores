using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip sound;
    public AudioSource sourcePlayer;
    [Range(0f, 1f)]
    public float volume = 1f;

    public void Play()
    {
        if (sound == null) return;
        sourcePlayer.volume= volume;
        sourcePlayer.PlayOneShot(sound);
    }
    public void SetPlayClip(AudioClip soundToBePlayed = null) 
    {
        if (soundToBePlayed == null) soundToBePlayed=sound;
        if (soundToBePlayed == null) return;
        sourcePlayer.volume = volume;
        sourcePlayer.PlayOneShot(soundToBePlayed);
    }
}
