using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLightPop : MonoBehaviour
{
    public GameObject lightBulbEmission;
    public Light lightToPop;
    public AudioSource lightPopSFX;
    public bool shouldPop;
    public float poppedLightIntensity;

    private Color lightColor;

    private bool alreadyPopped;

    void Start()
    {
        lightColor = new Color(252f/255f, 253f/255f, 178f/255f);
    }

    void Update()
    {
        if (shouldPop && !alreadyPopped)
        {
            Pop();
        }
    }

    private void Pop()
    {
        alreadyPopped = true;
        lightToPop.intensity = poppedLightIntensity;
        lightPopSFX.Play();
        lightBulbEmission.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * 0);
    }
}
