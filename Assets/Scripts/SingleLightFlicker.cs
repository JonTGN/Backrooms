using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLightFlicker : MonoBehaviour
{
    public Light light0;
    public Light light1;
    public GameObject light_bulb_emission;
    public float emissionIntensity; // to lazy to grab in script just set manually
    public float maxWaitTime = 1f;
    public AudioSource flickerSound;
    public bool shouldStartFlickering;
    private bool isFlickering;
    private float light0Intensity;
    private float light1Intensity;
    private Color lightColor;

    void Start()
    {
        light0Intensity = light0.intensity;
        light1Intensity = light1.intensity;

        lightColor = new Color(225f/255f, 253f/255f, 206f/255f); // color takes value 0-1 not 0-255
    }

    void Update() 
    {
        if (!isFlickering && shouldStartFlickering)
        {
            float waitTime;

            // Generate random time before next flicker
            waitTime = Random.Range(0.1f, maxWaitTime);

            StartCoroutine(Flicker(waitTime));
        }
    }

    private IEnumerator Flicker(float waitTime)
    {
        isFlickering = true;

        yield return new WaitForSeconds(waitTime);

        flickerSound.Play();

        light0.intensity = Random.Range(0, 2f);
        light1.intensity = Random.Range(0, 2f);

        // set emission values
        light_bulb_emission.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * 0); // *2 to better normalize values between light intensity and emission intensity

        yield return new WaitForSeconds(0.2f);

        light0.intensity = light0Intensity;
        light1.intensity = light1Intensity;

        light_bulb_emission.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * emissionIntensity);

        isFlickering = false;
    }
}
