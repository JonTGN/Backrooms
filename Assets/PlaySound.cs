using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource sound;

    public void PlaySoundEffect()
    {
        sound.Play();
    }

    public void PlayNoSound()
    {
        
    }
}
