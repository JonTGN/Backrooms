using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkRenderer : MonoBehaviour
{
    public string LevelTag;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == LevelTag)
        {
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == LevelTag)
        {

        }
    }
}
