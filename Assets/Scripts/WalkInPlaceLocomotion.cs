using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkInPlaceLocomotion : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private GameObject leftHand, rightHand;

    private Vector3 previousPosLeft, previousPosRight, direction;

    private Vector3 gravity = new Vector3(0, -9.8f, 0);

    [SerializeField] private float speed = 4;
    [SerializeField] private float velocityTriggerAmount = 0.05f;

    bool hasSwungOnce = false;

    public InputActionReference toggleReferenceRight = null;
    public InputActionReference toggleReferenceLeft = null;

    private bool leftTriggerPressed;
    private bool rightTriggerPressed;

    private void Awake()
    {
        toggleReferenceLeft.action.started += ToggleLeft;
        toggleReferenceRight.action.started += ToggleRight;
        toggleReferenceLeft.action.canceled += ToggleLeft;
        toggleReferenceRight.action.canceled += ToggleRight;
    }

    private void OnDestroy()
    {
        toggleReferenceLeft.action.started -= ToggleLeft;
        toggleReferenceRight.action.started -= ToggleRight;
        toggleReferenceLeft.action.canceled -= ToggleLeft;
        toggleReferenceRight.action.canceled -= ToggleRight;
    }

    void Start()
    {
        SetPreviousPos();
        leftTriggerPressed = false;
        rightTriggerPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the velocity of the player hand movement
        Vector3 leftHandVelocity = leftHand.transform.position - previousPosLeft;
        Vector3 rightHandVelocity = rightHand.transform.position - previousPosRight;
        float totalVelocity =+ leftHandVelocity.magnitude * 0.8f +rightHandVelocity.magnitude * 0.8f;

        if (leftTriggerPressed && rightTriggerPressed)
        {
            if (totalVelocity >= velocityTriggerAmount) //If true, player has swung their hands
            {

                //getting the direction that the player is facing
                direction = Camera.main.transform.forward;

                //move the player using the character controller
                characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up));
            }
        }

        //Applying gravity
        characterController.Move(gravity * Time.deltaTime);
        SetPreviousPos();
    }


    void SetPreviousPos()
    {
        previousPosLeft = leftHand.transform.position;
        previousPosRight= rightHand.transform.position;
    }

    private void ToggleLeft(InputAction.CallbackContext context)
    {
        leftTriggerPressed = !leftTriggerPressed;
    }

    private void ToggleRight(InputAction.CallbackContext context)
    {
        rightTriggerPressed = !rightTriggerPressed;
    }
}
