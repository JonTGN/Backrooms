using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl6Triggers : MonoBehaviour
{
    public bool openSideDoor;
    public bool activateSuitCrawl;
    public bool closeDoor;
    public bool mannequinLeg;

    public Animator sideDoor;
    public Animator suitAnim;
    public Lvl6PlaySounds trigger;
    public SingleLightFlicker lightFlickerScript;
    public GameObject manInSuit;
    public GameObject monster;
    public AudioSource monsterScream;
    public AudioSource hit;
    public Animator lvl7Door;
    public DoorShutSoundEffect lvl7DoorScript;
    public DoorShutSoundEffect sideDoorScript; 
    public Animator mannequinAnim;

    // fluorescent light
    public GameObject tube0Emission;
    public GameObject tube1Emission;
    public Light area0;
    public Light area1;
    public Light pointLight;
    public AudioSource fluorPop;

    // can light
    public GameObject canLightEmission;
    public Light canPoint0;
    public Light canPoint1;
    public AudioSource canPop;

    public Light TempPointLight;
    public bool hasPlayedJumpscare;



    private bool alreadyPlayed;
    private bool monsterSeqPlayed;
    private Color lightColor;

    void Start()
    {
        if (activateSuitCrawl)
            suitAnim.SetBool("idle", true);

        if (openSideDoor)
            lightColor = new Color(252f/255f, 253f/255f, 178f/255f);
    }
    void Update()
    {
        if (trigger.animIsDone && !monsterSeqPlayed)
        {
            // make suit disappear
            manInSuit.SetActive(false);

            // blow up lights (flour. & recessed @ doorway)
            BlowUpLights();

            // wait a bit
            // make monster appear & scream at you
            Invoke(nameof(MonsterCharge), 0f);

            monsterSeqPlayed = true;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (openSideDoor && !alreadyPlayed)
            {
                alreadyPlayed = true;
                sideDoor.SetBool("open", true);
                lightFlickerScript.maxWaitTime = 0;
                Invoke(nameof(ResetSideDoor), 2f);
            }

            if (activateSuitCrawl && !alreadyPlayed)
            {
                suitAnim.SetBool("crawl", true);
            }

            if (closeDoor && !alreadyPlayed)
            {
                sideDoor.SetBool("close", true);
                Invoke(nameof(OpenMainDoor), 1f);
                Invoke(nameof(ResetVar), 2f);
            }

            if (mannequinLeg && !alreadyPlayed)
            {
                mannequinAnim.SetBool("open", true);
            }
        }
    }

    private void ResetSideDoor()
    {
        sideDoorScript.playCreakSound = false;
    }

    private void ResetVar()
    {
        lvl7DoorScript.playCreakSound = false;
    }

    private void OpenMainDoor()
    {
        lvl7Door.SetBool("open", true);
    }

    private void MonsterCharge()
    {
        monster.SetActive(true);
        monsterScream.Play();
        hit.Play();

        Invoke(nameof(TurnOnLight), 2.5f);
    }

    private void BlowUpLights()
    {
        tube0Emission.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * 0);
        tube1Emission.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * 0);
        canLightEmission.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * 0);

        area0.enabled = false;
        area1.enabled = false;
        pointLight.enabled = false;
        canPoint0.enabled = false;
        canPoint1.enabled = false;
        TempPointLight.enabled = true;

        fluorPop.Play();
        canPop.Play();
    }

    private void TurnOnLight()
    {
        monster.SetActive(false);

        tube0Emission.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * 77);
        tube1Emission.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * 77);

        area0.enabled = true;
        area1.enabled = true;
        pointLight.enabled = true;

        canLightEmission.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * 77);
        canPoint0.enabled = true;
        canPoint1.enabled = true;


        TempPointLight.enabled = false;

        lightFlickerScript.maxWaitTime = 1.5f;
        hasPlayedJumpscare = true;
    }
}
