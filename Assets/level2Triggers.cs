using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2Triggers : MonoBehaviour
{
    public AudioSource playSound;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playSound.Play();
        }
    }

}
