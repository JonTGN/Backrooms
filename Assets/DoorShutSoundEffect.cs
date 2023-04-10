using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorShutSoundEffect : MonoBehaviour
{
    public AudioSource doorShutSFX;
    public AudioSource playCreak;
    public bool playCreakSound;
    public void PlayDoorShutSound()
    {
        if (!playCreakSound)
            doorShutSFX.Play();
        
        else
            playCreak.Play();
    }
}
