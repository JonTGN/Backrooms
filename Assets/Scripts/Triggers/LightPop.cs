using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPop : MonoBehaviour
{
    public GameObject lightBulbEmission;
    public Light lightToPop;
    public Light light0;
    public Light light1;
    public AudioSource lightPopSFX;
    public bool shouldPop;

    private Color lightColor;

    private bool alreadyPopped;

    void Start()
    {
        lightColor = new Color(225f/255f, 253f/255f, 206f/255f);
    }

    void Update()
    {
        if (shouldPop && !alreadyPopped)
        {
            Pop();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!alreadyPopped)
                Pop();
        }
    }

    private void Pop()
    {
        alreadyPopped = true;
        lightToPop.intensity = 0;
        light0.intensity = 0;
        light1.intensity = 0;
        lightPopSFX.Play();
        lightBulbEmission.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * 0);
    }
}
