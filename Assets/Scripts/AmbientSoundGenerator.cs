using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundGenerator : MonoBehaviour
{
    public List<AudioClip> audioClips;
    private AudioClip currentClip;
    public GameObject src;
    public AudioSource source;
    public float minWaitBetweenPlays = 1f;
    public float maxWaitBetweenPlays = 5f;
    public float maxSrcDist = 3f;
    private float waitTimeCountdown = -1f;
  
    void Update()
    {
        if (!source.isPlaying)
        {
            if (waitTimeCountdown < 0f)
            {
                // set position of src away from this GO
                var newPos = new Vector3(
                    transform.position.x + Random.Range(-maxSrcDist, maxSrcDist), 
                    transform.position.y, 
                    transform.position.z + Random.Range(-maxSrcDist, maxSrcDist)
                );

                src.transform.position = newPos;

                currentClip = audioClips[Random.Range(0, audioClips.Count)];
                source.clip = currentClip;
                source.Play();
                waitTimeCountdown = Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays);
            }
            else
            {
                waitTimeCountdown -= Time.deltaTime;
            }
        }
    }
}
