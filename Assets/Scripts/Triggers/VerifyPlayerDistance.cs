using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyPlayerDistance : MonoBehaviour
{
    Camera plrCam;
    float angle;
    bool readyToHide;
    bool isLookingInRightDirection;
    [SerializeField] private GetPlayerAngle getPlayerAngle;

    public GameObject elevator;
    public GameObject ceiling;
    public Lullaby lullaby;
    private bool triggerDone;

    void Start()
    {
        plrCam = Camera.main;
        getPlayerAngle.shouldCheckForAngle = true;
    }

    void Update()
    {
        CheckDirection(); // check if player is facing correct way

        if (readyToHide && isLookingInRightDirection && !triggerDone)
        {
            // hide elevator and show rest of hallway
            elevator.SetActive(false);
            ceiling.SetActive(true);
            getPlayerAngle.shouldCheckForAngle = false;
            lullaby.StartSong();
            triggerDone = true;
        }
    }

    private void CheckDirection()
    {
        angle = getPlayerAngle.angle;

        if (angle > 20 && angle < 150)
            isLookingInRightDirection = true;
        else
            isLookingInRightDirection = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // player has passed trigger, can hide elevator if looking in right dir
            if (plrCam.transform.position.x > gameObject.transform.position.x)
                readyToHide = true;

            // player has went back into the elevator
            else
                readyToHide = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // player has entered trigger at this spot they are going back in the elevator
            if (plrCam.transform.position.x > gameObject.transform.position.x)
                readyToHide = false;
        }
    }
}
