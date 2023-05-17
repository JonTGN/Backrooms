using UnityEngine;
using System.Collections;

public class DualLightFlickerJumpscare : MonoBehaviour {
    public new Light light0;
    public new Light light1;
    public GameObject light_bulb_emission0;
    public GameObject light_bulb_emission1;
    public float emissionIntensity; // to lazy to grab in script just set manually
    public float maxWaitTime = 1f;
    public AudioSource flickerSound;
    public bool shouldStartFlickering;

    // handle jumpscare here so it's in time w/ the lights
    public bool readyForJumpscare;
    public GameObject jumpscare;
    public AudioSource jumpscareHit;
    public GameObject radioAudio; // disable when jumpscare happens
    private bool AlreadyPlayedJumpscare;

    private bool isFlickering;
    private float light0Intensity;
    private float light1Intensity;
    private Color lightColor;

    void Start()
    {
        light0Intensity = light0.intensity;
        light1Intensity = light1.intensity;

        lightColor = new Color(224f/255f, 219f/255f, 143f/255f); // color takes value 0-1 not 0-255
    }

    void Update() {

        if (!isFlickering && shouldStartFlickering)
        {
            float waitTime;

            // Generate random time before next flicker
            if (readyForJumpscare && !AlreadyPlayedJumpscare)
                waitTime = 0.1f;
            else
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
        light_bulb_emission0.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * 0); // *2 to better normalize values between light intensity and emission intensity
        light_bulb_emission1.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * 0);

        // jumpscare
        Debug.Log("ready: "+ readyForJumpscare + " already played: " + AlreadyPlayedJumpscare);
        if (readyForJumpscare && !AlreadyPlayedJumpscare)
        {
            Debug.Log("jumpscare");
            jumpscare.SetActive(true);
            jumpscareHit.Play();
            radioAudio.SetActive(false); // stop lullaby if still playing
            AlreadyPlayedJumpscare = true;
        }

        yield return new WaitForSeconds(0.2f);

        light0.intensity = light0Intensity;
        light1.intensity = light1Intensity;

        light_bulb_emission0.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * emissionIntensity);
        light_bulb_emission1.GetComponent<Renderer>().material.SetColor("_EmissiveColor", lightColor * emissionIntensity);

        isFlickering = false;

        if (readyForJumpscare && AlreadyPlayedJumpscare)
        {
            jumpscare.SetActive(false);
        }
    }

}