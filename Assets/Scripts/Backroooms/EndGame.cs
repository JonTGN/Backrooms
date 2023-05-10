using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private GameObject oldPlr;
    public GameObject newPlr;

    public GameObject monster;
    public AudioSource jumpscareSoundEffect;
    public Animator monsterAnim;

    private void Start()
    {
        oldPlr = Camera.main.gameObject.transform.parent.gameObject.transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            oldPlr.SetActive(false);
            newPlr.SetActive(true);

            newPlr.GetComponent<CharacterController>().enabled = false;

            Invoke(nameof(TriggerJumpscare), 2f);
        }
    }

    void TriggerJumpscare()
    {
        monster.SetActive(true);
        jumpscareSoundEffect.Play();
        monsterAnim.SetBool("idle", true);
        monsterAnim.speed = 2f;

        Invoke(nameof(StopJumpscare), 2.3f);
    }

    void StopJumpscare()
    {
        monster.SetActive(false);

        SceneManager.LoadScene("black");
         
    }
}
