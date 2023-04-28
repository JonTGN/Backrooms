using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackroomsElevatorController : MonoBehaviour
{
    public Animator anim;
    public GameObject elevator;
    public AudioSource elevator_open;


    void Start()
    {
        Invoke(nameof(OpenDoor), 4f);
    }

    void OpenDoor()
    {
        anim.SetBool("open", true);
        elevator_open.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        elevator.SetActive(false);
    }
}
