using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyPlayerDistance : MonoBehaviour
{
    Camera plrCam;
    float angle;
    bool readyToHide;
    bool isLookingInRightDirection;
    public GetPlayerAngle getPlayerAngle;
    public light_creak light_Creak;

    public GameObject go_to_hide;
    public GameObject go_to_show;
    public AudioSource radio;
    private bool triggerDone;

    void Start()
    {
        plrCam = Camera.main;
        getPlayerAngle.shouldCheckForAngle = true;
    }

    void Update()
    {
        /*
        CheckDirection(); // check if player is facing correct way

        if (readyToHide && isLookingInRightDirection && !triggerDone)
        {
            // hide elevator and show rest of hallway
            go_to_hide.SetActive(false);
            go_to_show.SetActive(true);
            getPlayerAngle.shouldCheckForAngle = false;
            radio.Play();
            light_Creak.isActive = true;
            triggerDone = true;
        }
        */
    }

    private void CheckDirection()
    {
        //angle = getPlayerAngle.angle;

        //if (angle > 120 && angle < 230)
        //    isLookingInRightDirection = true;
        //else
        //    isLookingInRightDirection = false;
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    // player has passed trigger, can hide elevator if looking in right dir
        //    if (plrCam.transform.position.x > gameObject.transform.position.x)
        //        readyToHide = true;

        //    // player has went back into the elevator
        //    else
        //        readyToHide = false;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //// player has entered trigger at this spot they are going back in the elevator
            //if (plrCam.transform.position.x > gameObject.transform.position.x)
            //    readyToHide = false;
            ShowHallway();
        }
    }

    private void ShowHallway()
    {
        // hide elevator and show rest of hallway
        go_to_hide.SetActive(false);
        go_to_show.SetActive(true);
        getPlayerAngle.shouldCheckForAngle = false;
        radio.Play();
        light_Creak.isActive = true;
        triggerDone = true;
    }
}
