using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDTrigger : MonoBehaviour
{
    private bool hasActivated;
    public Animator keycardDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("keycard") && !hasActivated)
        {
            keycardDoor.SetBool("open", true);
        }
    }
}
