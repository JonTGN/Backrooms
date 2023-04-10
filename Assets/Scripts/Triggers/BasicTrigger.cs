using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTrigger : MonoBehaviour
{
    public GameObject go1; // will be shown on default
    public GameObject go2; // will be hidden on default
    public GameObject player;

    public bool state;

    void Start()
    {
        go1.SetActive(true);
        go2.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // assume player will continue walking in expected direction
            go1.SetActive(state);
            go2.SetActive(!state);
            state = !state;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // player is behind GO
            if (player.transform.position.x < gameObject.transform.position.x)
            {
                go1.SetActive(false);
                go2.SetActive(true);
            }

            // player is in front
            else
            {
                go1.SetActive(true);
                go2.SetActive(false);
            }
        }
    }
}
