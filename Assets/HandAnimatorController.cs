using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorController : MonoBehaviour
{
    public InputActionProperty gripAction;
    public InputActionProperty triggerAction;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        float triggerValue = triggerAction.action.ReadValue<float>();
        float gripValue = gripAction.action.ReadValue<float>();

        //anim.SetFloat("Trigger", triggerValue);
        //anim.SetFloat("Grip", gripValue);

        float btnValue;
        if (triggerValue > 0)
            btnValue = triggerValue;
        else
            btnValue = gripValue;

        anim.SetFloat("Grip", btnValue);
    }


}
