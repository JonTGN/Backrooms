using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    public bool OpenDoor = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("testing trigger enter");

        if (other.tag == "keycard")
        {
            OpenDoor = true;
            Debug.Log("Door open");
        }
    }
}
