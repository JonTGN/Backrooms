using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Animator anim;
    public AudioSource elevator_open;
    //public AudioSource elevator_ding;
    public AudioSource game_start;

    public bool skipIntro;
    public DualLightFlickerJumpscare lightScript;

    void Start()
    {
        // todo: trigger sounds on anim events
        //Invoke(nameof(Ding), 0.5f);
        game_start.Play();

        Invoke(nameof(OpenSound), 61);
        Invoke(nameof(OpenDoor), 61);

        if (skipIntro)
        {
            OpenDoor();
            CancelInvoke();
        }
    }

    void OpenDoor()
    {
        anim.SetBool("open", true);
        lightScript.shouldStartFlickering = true;
    }

    void Ding()
    {
        //elevator_ding.Play();
    }

    void OpenSound()
    {
        elevator_open.Play();
    }
    
}
