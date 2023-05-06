using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnLight : MonoBehaviour
{
    public string ElevatorTag;
    public GameObject light1;
    public GameObject light2;
    public GameObject tube1;
    public GameObject tube2;

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("trigger enter");
        //Debug.Log("tag: " + other.gameObject.tag);
        //Debug.Log("info: " + other.gameObject.name);
        if (other.gameObject.tag == ElevatorTag)
        {
            light1.SetActive(true);
            light2.SetActive(true);

            tube1.SetActive(true);
            tube2.SetActive(true);
        }
    }
}
