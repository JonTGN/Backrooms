using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl5Triggers : MonoBehaviour
{
    public bool lightBreakTrigger;
    public bool lightTurnOnTrigger;

    public GetPlayerAngle plrAngle;
    public AudioSource horrorSweepSFX;
    public SingleLightPop lightToPop;
    public GameObject lightEmission;
    public Light lightObject;
    public AudioSource lightFlicker;
    public GameObject monster;

    private bool alreadyPlayed;
    private bool playedSound;
    private Color lightColor;

    void Start()
    {
        lightColor = new Color(225f/255f, 253f/255f, 206f/255f);
    }
    void Update()
    {
        if (alreadyPlayed && lightBreakTrigger && !playedSound)
        {
            if (plrAngle.angle > 125 && plrAngle.angle < 245)
            {    
                PlaySound();
                playedSound = true;
            }

        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (lightBreakTrigger && !alreadyPlayed)
            {
                // break light
                lightToPop.shouldPop = true;

                monster.SetActive(true);

                alreadyPlayed = true;
            }

            else if (lightTurnOnTrigger && !alreadyPlayed)
            {
                alreadyPlayed = true;

                lightEmission.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * 60);
                lightObject.enabled = false;
                lightObject.enabled = true;
                lightObject.intensity += 25;

                monster.SetActive(false);
                lightFlicker.Play();

            }
        }
    }

    private void PlaySound()
    {
        horrorSweepSFX.Play();
    }
}
