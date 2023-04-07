using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorThumpTrigger : MonoBehaviour
{
    public GameObject thumping;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            thumping.SetActive(true);
        }
    }
}
