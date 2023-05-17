using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedupanim : MonoBehaviour
{
    public Animator anim;
    public float speedOfAnim;

    void Start()
    {
        anim.speed = speedOfAnim;
    }

    void Update()
    {
        anim.speed = speedOfAnim;
    }

    
}
