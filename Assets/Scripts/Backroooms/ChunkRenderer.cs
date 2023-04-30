using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkRenderer : MonoBehaviour
{
    public string LevelTag;
    public GenerationManager genMngr;
    public GameObject WorldGrid;
    public Transform WorldGridParents;

    private bool alreadySpawnedElevator;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == LevelTag)
        {
            genMngr.WorldGrid = other.gameObject.transform;

            if (!alreadySpawnedElevator)
            {
                genMngr.GenerateWorld(true);
                alreadySpawnedElevator = true;
            }
            else
                genMngr.GenerateWorld(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == LevelTag)
        {
            Vector3 oldPos = other.gameObject.transform.position;  // store old pos
            Destroy(other.gameObject);  //unload chunk
            Instantiate(WorldGrid, oldPos, Quaternion.identity, WorldGridParents);  // spawn empty chunk
            //Debug.Log("spawning old world grid at: " + oldPos.x + ", " + oldPos.z);
        }
    }
}
