using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightJumpscareTrigger : MonoBehaviour
{
    public DualLightFlickerJumpscare flickerScript;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            flickerScript.readyForJumpscare = true;
        }
    }
}
