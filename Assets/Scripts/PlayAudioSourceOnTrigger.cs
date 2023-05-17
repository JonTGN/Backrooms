using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSourceOnTrigger : MonoBehaviour
{
    public AudioSource sound;
    
    private bool activated;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !activated)
        {
            activated = true;
            sound.Play();
        }
    }
}
