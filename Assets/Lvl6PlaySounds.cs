using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl6PlaySounds : MonoBehaviour
{
    public AudioSource bones;
    public AudioSource scream;
    public bool animIsDone;

    public void PlayBonesCracking()
    {
        bones.Play();
    }

    public void PlayScream()
    {
        scream.Play();
    }

    public void AnimDone()
    {
        animIsDone = true;
    }
}
