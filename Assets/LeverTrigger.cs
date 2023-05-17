using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    public GameObject objectToHide;
    public GameObject objectToShow;
    public Animator elevatorAnim;
    public AudioSource LeverHitSound;
    public AudioSource ElevatorOpenSound;
    public AudioSource Siren;

    private bool activated;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Lever") && !activated)
        {
            activated = true;
            objectToHide.SetActive(false);
            objectToShow.SetActive(true);

            LeverHitSound.Play();
            Invoke(nameof(PlayElevatorSound), 0f);
            Invoke(nameof(PlaySound), 1f);
        }
    }

    private void PlayElevatorSound()
    {
        elevatorAnim.SetBool("open", true);
        ElevatorOpenSound.Play();
    }

    private void PlaySound()
    {
        Siren.Play();
    }
}
