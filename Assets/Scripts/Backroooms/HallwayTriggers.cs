using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayTriggers : MonoBehaviour
{
    public GameObject outerRoom;
    public bool hallwayHasTriggered;
    public AudioSource[] lightPops;
    public GameObject Player;
    public GameObject bulb; 
    public GameObject bulb1;
    public Light light0;
    public Light light1;
    public AudioSource LightBulbInHallwayAS;
    public GameObject GeneratedLightsParent;
    public Animator MonsterAnim;
    public MonsterChase monsterChase;

    private bool alreadyPlayed;

    void Start()
    {
        Player = Camera.main.transform.parent.parent.gameObject;
        Debug.Log("Player is: " + Player.name);

        GeneratedLightsParent = GameObject.FindWithTag("GeneratedLightsTag");
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !alreadyPlayed)
        {
            outerRoom.SetActive(true);
            alreadyPlayed = true;

            StartCoroutine(DestroyLights());

            // remove area around hallway to extend it
            Player.GetComponentInChildren<ChunkRenderer>().RemoveOutsideLevel();

            // disable chunk renderer
            Player.GetComponentInChildren<ChunkRenderer>().gameObject.SetActive(false);
            Player.GetComponentInChildren<ReplaceLightsWhenClose>().gameObject.SetActive(false);

            Invoke(nameof(StartMonsterChase), 3f);
        }
    }

    private void StartMonsterChase()
    {
        MonsterAnim.SetBool("charge", true);
        
        monsterChase.ShouldChase = true;
    }

    public IEnumerator DestroyLights()
    {
        WaitForSeconds wait = new WaitForSeconds(0f);

        GeneratedLightsParent.SetActive(false);

        foreach (var light in lightPops)
        {
            light.Play();
            wait = new WaitForSeconds(Random.Range(0.01f, 0.05f));
            yield return wait;
        }

        // after for loop make separate ref to last audio source and break it and the lights
        LightBulbInHallwayAS.Play();
        bulb.GetComponent<Renderer>().material.SetColor("_EmissiveColor", Color.black * 0);
        bulb1.GetComponent<Renderer>().material.SetColor("_EmissiveColor", Color.black * 0);
        light0.intensity = 0;
        light1.intensity = 0;

        // play monster sound? make hallway behind you bigger? 
    }
}
