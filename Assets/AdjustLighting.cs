using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustLighting : MonoBehaviour
{
    public Light[] lights;
    public GameObject[] lightEmissions;

    private Color redEmission = new Color(248f/255f, 107f/255f, 107f/255f);
    

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            foreach (Light light in lights)
            {
                // turn each light to red
                light.color = redEmission;
                light.intensity += 50;
            }

            foreach (GameObject lightEmission in lightEmissions)
            {
                // adjust emissions for each light
                lightEmission.GetComponent<Renderer>().material.SetColor("_EmissiveColor", redEmission);
            }
        }
    }
}
