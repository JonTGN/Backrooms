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

    void Start()
    {
        // todo: trigger sounds on anim events
        //Invoke(nameof(Ding), 0.5f);
        if (skipIntro)
        {
            OpenDoor();
            return;
        }

        game_start.Play();

        Invoke(nameof(OpenSound), 61);
        Invoke(nameof(OpenDoor), 61);
    }

    void OpenDoor()
    {
        anim.SetBool("open", true);
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
