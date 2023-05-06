using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    public bool ShouldChase;
    public float Speed = 1f;

    void Update()
    {
        if (ShouldChase)
        {
            transform.position += new Vector3(0, 0, -1f) * Speed * Time.deltaTime;
        }
    }
}
