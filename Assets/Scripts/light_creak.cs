using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_creak : MonoBehaviour
{
    public AudioSource forwardsCreak;
    public AudioSource backwardsCreak;
    public bool isActive;

    public void playForwardCreak()
    {
        if (isActive)
            forwardsCreak.Play();
    }

    public void playBackwardsCreak()
    {
        if (isActive)
            backwardsCreak.Play();
    }
}
