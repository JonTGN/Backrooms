using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl7Triggers : MonoBehaviour
{
    public bool changeLights;
    public bool fallThroughCeiling;

    public GameObject oldLights;
    public GameObject newLights;
    public AudioSource sceneChangeSFX;
    public Animator fallAnim;
    public AudioSource fallSFX;
    public AudioSource RobotVoice;


    private bool hasActivated;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (changeLights && !hasActivated)
            {
                oldLights.SetActive(false);
                newLights.SetActive(true);

                sceneChangeSFX.Play();
                RobotVoice.Play();

                hasActivated = true;
            }

            if (fallThroughCeiling && !hasActivated)
            {
                fallAnim.SetBool("fall", true);
                fallSFX.Play();

                hasActivated = true;
            }
        }
    }
}
