using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GenerationState
{
    Idle,
    GeneratingRooms,
    GeneratingLighting,
    GeneratingSpawn
}

public class GenerationManager : MonoBehaviour
{
    [Header("References")]
    public Transform WorldGrid;
    [SerializeField] List<GameObject> RoomTypes;
    [SerializeField] List<GameObject> LightTypes;
    [SerializeField] int mapSize = 81;
    [SerializeField] Slider MapSizeSlider, EmptinessSlider, BrightnessSlider;
    [SerializeField] Button GenerateButton;
    [SerializeField] GameObject EmptyRoom;
    [SerializeField] GameObject SpawnRoom;
    public List<GameObject> GeneratedRooms; // store generated rooms to replace w/ spawn
    [SerializeField] GameObject PlayerObject, MainCameraObject;
    
    [Header("Settings")]
    public int mapEmptiness = 4; // chance of empty room spawning
    public int mapBrightness = 4; // chance of light spawning
    private int mapSizeSqr = 9; 
    private float currentPosX, currentPosZ, currentPosTracker; // keep track of room gen pos
    public float roomSize = 7;
    private Vector3 currentPos; // current pos of room to be gen
    public GenerationState currentState; // current gen state

    private void Update()  // ui necessary for testing, not so much anymore
    {
        //mapSize = (int)Mathf.Pow(MapSizeSlider.value, 4);

        //mapSizeSqr = (int)Mathf.Sqrt(mapSize);

        //mapEmptiness = (int)EmptinessSlider.value;

        //mapBrightness = (int)BrightnessSlider.value;
    }

    private void Start()
    {
        for (int i = 0; i < mapEmptiness; i++)
        {
            RoomTypes.Add(EmptyRoom);
        }
    }
  
    public void ReloadWorld()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GenerateWorld(bool initLoad)
    {
        //Debug.Log("generating room...");
        GeneratedRooms = new List<GameObject>();
        //Debug.Log("gen world...");
        //Debug.Log("world pos: " + WorldGrid.position.x + ", " + WorldGrid.position.z);

        currentPosX = WorldGrid.position.x;
        currentPosZ = WorldGrid.position.z;

        for (int state = 0; state < 5; state++)
        {
            for (int i = 0; i < mapSize; i++)
            {
                if (currentPosTracker == mapSizeSqr)
                {
                    currentPosX = WorldGrid.position.x;
                    currentPosTracker = 0;

                    currentPosZ += roomSize;
                    //Debug.Log("new x: " + currentPosX);
                    //Debug.Log("new z: " + currentPosZ);
                }

                currentPos = new(currentPosX, 0, currentPosZ);
                //Debug.Log("Spawned room at: " + currentPos.x + ", " + currentPos.z);

                switch (currentState)
                {
                    case GenerationState.GeneratingRooms:
                        //if (i == 0) Debug.Log("current pos: " + currentPos.x + ", " + currentPos.z);

                        Quaternion roomRotation = Quaternion.identity;
                        int rot = Random.Range(0, 4);

                        roomRotation *= Quaternion.Euler(Vector3.up * (90 * rot)); // rotate the room 0/90/180/270 degrees

                        GeneratedRooms.Add(Instantiate(RoomTypes[Random.Range(0, RoomTypes.Count)], currentPos, roomRotation, WorldGrid));
                    break;

                    case GenerationState.GeneratingLighting:
                        int lightSpawn = Random.Range(-1, mapBrightness);

                        if (lightSpawn == 0 && (currentPos.x != 0 && currentPos.z != 0))
                            Instantiate(LightTypes[Random.Range(0, LightTypes.Count)], currentPos, Quaternion.identity, WorldGrid);
                    break;
                }

                currentPosTracker++;
                currentPosX += roomSize;
            }

            NextState();

            switch (currentState)
            {
                case GenerationState.GeneratingSpawn:
                    if (initLoad) PickSpawnRoom();
                    break;
            }
        }

        currentState = GenerationState.Idle;
    }

    public void PickSpawnRoom()
    {
        Debug.Log("picking spawn...");
        int roomToReplace = 0; //Random.Range(0, GeneratedRooms.Count - mapSizeSqr - 1);
        // spawn empty room right under this room, cap spawn room gen so it doesn't gen on bottom row

        // spawn empty room directly in direction of spawn rooms doorway to ensure 100% exit
        int roomToReplaceWithEmpty = roomToReplace + 9;

        spawnRoom = Instantiate(SpawnRoom, GeneratedRooms[roomToReplace].transform.position, Quaternion.identity, WorldGrid);
        Destroy(GeneratedRooms[roomToReplace]);

        // spawn empty room at correct spot
        var empty = Instantiate(EmptyRoom, GeneratedRooms[roomToReplaceWithEmpty].transform.position, Quaternion.identity, WorldGrid);
        Destroy(GeneratedRooms[roomToReplaceWithEmpty]);

        GeneratedRooms[roomToReplaceWithEmpty] = empty;
        GeneratedRooms[roomToReplace] = spawnRoom;

        SpawnPlayer();
    }

    public GameObject spawnRoom;

    public void SpawnPlayer()
    {
        //PlayerObject.SetActive(false);

        PlayerObject.transform.position = spawnRoom.transform.position;

        PlayerObject.SetActive(true);
        MainCameraObject.SetActive(false);
    }

    public void NextState()
    {
        currentState++;

        currentPosX = WorldGrid.position.x;
        currentPosZ = WorldGrid.position.z;
        currentPosTracker = 0;
        currentPos = WorldGrid.position;
    }
}
