using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Animator anim;
    public AudioSource elevator_open;
    public AudioSource elevator_ding;

    void Start()
    {
        // todo: trigger sounds on anim events
        Invoke(nameof(Ding), 0.5f);
        Invoke(nameof(OpenSound), 1.8f);
        Invoke(nameof(OpenDoor), 2);
    }

    void OpenDoor()
    {
        anim.SetBool("open", true);
    }

    void Ding()
    {
        elevator_ding.Play();
    }

    void OpenSound()
    {
        elevator_open.Play();
    }
    
}
