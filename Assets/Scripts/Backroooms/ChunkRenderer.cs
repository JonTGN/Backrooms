using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChunkRenderer : MonoBehaviour
{
    public string LevelTag;
    public GenerationManager genMngr;
    public GameObject WorldGrid;
    public Transform WorldGridParents;
    public GameObject InfiniteHallwayPrefab;
    public GameObject Player;
    public float GameDuration;
    public LayerMask normalRoom, worldGrid;

    private bool alreadySpawnedElevator;
    public bool shouldSpawnHallway;
    private bool hallwayIsSpawned;
    private Vector3 worldGridHallwayIsSpawnedUnder;
    public AudioSource HallwaySpawned;
    public GameObject infiniteHallwayReferenceInScene;  // destroy this when unloading chunk (instaniate to world grid parent!!)

    // keep track of vector3's of chunks to spawn hallway at optimal position
    public List<Vector3> WorldGridsSpawned;

    void Start()
    {
        Invoke(nameof(EnableHallwaySpawn), GameDuration);
    }

    void EnableHallwaySpawn()
    {
        shouldSpawnHallway = true;
        genMngr.ReadyToGenerateHall = true;
    }

    void SpawnHallway()
    {
        if (shouldSpawnHallway && !hallwayIsSpawned)
        {
            hallwayIsSpawned = true;
            HallwaySpawned.Play();

            // get furthest world grid on z (prioritize lowest z value, so don't have to mess w/ hall rotation)
            worldGridHallwayIsSpawnedUnder = 
                WorldGridsSpawned.OrderBy(p => p.z).First();

            // offset instead of finding/deleting chunk's pos0 GO prefab
            var offsetPos = new Vector3(worldGridHallwayIsSpawnedUnder.x, 0, worldGridHallwayIsSpawnedUnder.z - 3);

            //Debug.Log("lowest z worldgrid found: " + worldGridHallwayIsSpawnedUnder);

            // spawn hall on pos0
            infiniteHallwayReferenceInScene = Instantiate(InfiniteHallwayPrefab, offsetPos, Quaternion.identity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == LevelTag)
        {
            //Debug.Log("go tag: " + other.gameObject.tag);
            //Debug.Log("go name: " + other.gameObject.name);

            SpawnHallway(); 

            genMngr.WorldGrid = other.gameObject.transform;

            if (!alreadySpawnedElevator)
            {
                genMngr.GenerateWorld(true);
                alreadySpawnedElevator = true;
            }
            else
                genMngr.GenerateWorld(false);

            WorldGridsSpawned.Add(other.gameObject.transform.position);
        }

        //else 
        //{
        //    Debug.Log("go tag: " + other.gameObject.tag);
        //    Debug.Log("go name: " + other.gameObject.name);
        //}
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == LevelTag)
        {
            SpawnHallway();

            Vector3 oldPos = other.gameObject.transform.position;  // store old pos
            Destroy(other.gameObject);  //unload chunk
            Instantiate(WorldGrid, oldPos, Quaternion.identity, WorldGridParents);  // spawn empty chunk
            //Debug.Log("spawning old world grid at: " + oldPos.x + ", " + oldPos.z);

            //Debug.Log("unloading: " + oldPos);
            //Debug.Log("hallway @: " + worldGridHallwayIsSpawnedUnder);
            // check if chunk being unloaded contains hallway
            if (oldPos == worldGridHallwayIsSpawnedUnder)
            {
                var hallStartPos = new Vector3(oldPos.x, oldPos.y, oldPos.z - 3);
                var roomAbovePos = new Vector3(oldPos.x + 3, oldPos.y, oldPos.z - 3); // store position of room above hall, when there is none, no need to gen more rooms
                // before destroying hallway, replace each room with random room
                while (Physics.CheckSphere(hallStartPos, 0.5f, genMngr.hallwayLayer) && Physics.CheckSphere(roomAbovePos, 0.5f, normalRoom))
                {
                    Debug.Log("checking hall pos: " + hallStartPos);
                    Debug.Log("room abvove pos: " + roomAbovePos);

                    // need to instantiate under correct worldgrid
                    // check sphere for current collider, if tag == level, get GO and instantiate under that!
                    Collider[] worldGridInScene = Physics.OverlapSphere(hallStartPos, 0.5f, worldGrid);
                    var parent = worldGridInScene.First(); // should only be 1.. lazy dev

                    Instantiate(genMngr.RoomTypes[Random.Range(0, genMngr.RoomTypes.Count)], hallStartPos, Quaternion.identity, parent.gameObject.transform);
                    hallStartPos = new Vector3(hallStartPos.x, hallStartPos.y, hallStartPos.z - 3);
                    roomAbovePos = new Vector3(roomAbovePos.x, roomAbovePos.y, roomAbovePos.z - 3);
                }
                
                Debug.Log("failed checking hall pos: " + hallStartPos + " : " + Physics.CheckSphere(hallStartPos, 0.5f, genMngr.hallwayLayer));
                Debug.Log("faield room abvove pos: " + roomAbovePos + " : " + Physics.CheckSphere(roomAbovePos, 0.5f, normalRoom));

                Debug.Log("destroying hallway at: " + oldPos);
                Destroy(infiniteHallwayReferenceInScene);

                
                hallwayIsSpawned = false;
            }

            WorldGridsSpawned.Remove(other.gameObject.transform.position);
        }
    }

    public void RemoveOutsideLevel()
    {
        var HallStartPos = new Vector3(worldGridHallwayIsSpawnedUnder.x, worldGridHallwayIsSpawnedUnder.y, worldGridHallwayIsSpawnedUnder.z -3);
        Collider[] RoomsToRemove = Physics.OverlapSphere(HallStartPos, 50, normalRoom);

        foreach (var room in RoomsToRemove)
        {
            Destroy(room.gameObject);
        }
    }
}
