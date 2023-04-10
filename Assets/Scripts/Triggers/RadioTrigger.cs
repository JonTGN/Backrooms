using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTrigger : MonoBehaviour
{
    public AudioSource radio;
    public AudioClip light_static;
    public AudioClip heavy_static;
    public bool init;
    private bool hasTriggered;


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (init && !hasTriggered)
            {
                radio.clip = light_static;
                radio.Play();
                hasTriggered = true;
            }

            else if (!hasTriggered)
            {
                radio.volume = 0.4f;
                radio.clip = heavy_static;
                radio.Play();
                hasTriggered = true;
            }
        }
    }
}
