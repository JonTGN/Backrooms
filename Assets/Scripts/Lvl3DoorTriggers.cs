using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3DoorTriggers : MonoBehaviour
{
    public AudioSource crying;
    public GameObject thumping;
    public GameObject fastThumping;

    // bools
    public bool cryTrigger;
    public bool thumpTrigger;
    public bool stopThump;
    private bool alreadyPlayed;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (cryTrigger && !alreadyPlayed)
            {
                crying.Play();
                alreadyPlayed = true;
            }

            if (thumpTrigger && !alreadyPlayed)
            {
                thumping.SetActive(false);
                fastThumping.SetActive(true);
                alreadyPlayed = true;
            }

            if (stopThump && !alreadyPlayed)
            {
                fastThumping.SetActive(false);
                alreadyPlayed = true;
            }
        }
    }
}
