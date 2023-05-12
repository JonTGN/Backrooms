using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl4Triggers : MonoBehaviour
{
    public bool shutLvl5Door;
    public bool openSideDoor;
    public bool headBanging;
    public bool openLvl5Door;

    public Animator doorAnim;  // "close_o", "open"
    public Animator sideDoorAnim;
    public Animator maleInSuit;  // "head"
    public AudioSource creepyDrone;
    public GameObject creepyDroneGO;
    public GameObject oldThumping;
    public DoorShutSoundEffect doorScript;

    private bool alreadyPlayed;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (shutLvl5Door && !alreadyPlayed)
            {
                doorAnim.SetBool("close_o", true);
                alreadyPlayed = true;
            }

            if (openSideDoor && !alreadyPlayed)
            {
                Invoke(nameof(PlayAudio), 1f);
                Invoke(nameof(OpenDoor), 9f);  // open door anim calls sfx
                alreadyPlayed = true;
            }

            if (headBanging && !alreadyPlayed)
            {
                Invoke(nameof(HeadBanging), 9f);
                alreadyPlayed = true;
            }

            if (openLvl5Door && !alreadyPlayed)
            {
                doorScript.playCreakSound = true;
                doorAnim.SetBool("open", true);
                alreadyPlayed = true;
            }
        }
    }

    private void PlayAudio()
    {
        Debug.Log("insidep play audio");
        creepyDrone.Play();
        Invoke(nameof(DisableGO), 6f);
    }

    private void DisableGO()
    {
        creepyDroneGO.SetActive(false);
    }

    private void OpenDoor()
    {
        oldThumping.SetActive(false);
        sideDoorAnim.SetBool("open", true);
        alreadyPlayed = true;
    }

    private void HeadBanging()
    {
        maleInSuit.SetBool("head", true);
        alreadyPlayed = true;
    }
}
