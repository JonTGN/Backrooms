using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceLightsWhenClose : MonoBehaviour
{
    public string OriginalTag;
    public string LightTag;
    public GameObject lightGameObject;
    public GameObject GeneratedLightsParent;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == OriginalTag)
        {
            Instantiate(lightGameObject, other.transform.position, other.transform.rotation, GeneratedLightsParent.transform);
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == OriginalTag)
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }

        if (other.gameObject.tag == LightTag)
        {
            Destroy(other.gameObject);
        }
    }
}
