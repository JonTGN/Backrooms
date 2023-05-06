using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDTrigger : MonoBehaviour
{
    private bool hasActivated;
    public Animator keycardDoor;
    public AudioSource doorOpen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("keycard") && !hasActivated)
        {
            hasActivated = true;
            doorOpen.Play();
            Invoke(nameof(OpenDoor), 1.5f);
        }
    }

    private void OpenDoor()
    {
        keycardDoor.SetBool("open", true);
    }
}
