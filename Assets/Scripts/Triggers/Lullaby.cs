using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lullaby : MonoBehaviour
{
    public AudioSource lullaby;

    public void StartSong()
    {
        lullaby.Play();
    }
}
