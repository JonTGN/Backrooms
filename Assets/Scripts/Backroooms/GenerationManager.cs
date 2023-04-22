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
    [SerializeField] Transform WorldGrid;
    [SerializeField] List<GameObject> RoomTypes;
    [SerializeField] List<GameObject> LightTypes;
    [SerializeField] int mapSize = 16;
    [SerializeField] Slider MapSizeSlider, EmptinessSlider, BrightnessSlider;
    [SerializeField] Button GenerateButton;
    [SerializeField] GameObject EmptyRoom;
    [SerializeField] GameObject SpawnRoom;
    public List<GameObject> GeneratedRooms; // store generated rooms to replace w/ spawn
    [SerializeField] GameObject PlayerObject, MainCameraObject;
    
    [Header("Settings")]
    public int mapEmptiness; // chance of empty room spawning
    public int mapBrightness; // chance of light spawning
    private int mapSizeSqr; 
    private float currentPosX, currentPosZ, currentPosTracker; // keep track of room gen pos
    public float roomSize = 7;
    private Vector3 currentPos; // current pos of room to be gen
    public GenerationState currentState; // current gen state

    private void Update()
    {
        mapSize = (int)Mathf.Pow(MapSizeSlider.value, 4);

        mapSizeSqr = (int)Mathf.Sqrt(mapSize);

        mapEmptiness = (int)EmptinessSlider.value;

        mapBrightness = (int)BrightnessSlider.value;
    }
  
    public void ReloadWorld()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GenerateWorld()
    {
        for (int i = 0; i < mapEmptiness; i++)
        {
            RoomTypes.Add(EmptyRoom);
        }

        GenerateButton.interactable = false;

        for (int state = 0; state < 5; state++)
        {
            for (int i = 0; i < mapSize; i++)
            {
                if (currentPosTracker == mapSizeSqr)
                {
                    currentPosX = 0;
                    currentPosTracker = 0;

                    currentPosZ += roomSize;
                }

                currentPos = new(currentPosX, 0, currentPosZ);

                switch (currentState)
                {
                    case GenerationState.GeneratingRooms:
                        Quaternion roomRotation = Quaternion.identity;
                        int rot = Random.Range(0, 4);

                        roomRotation *= Quaternion.Euler(Vector3.up * (90 * rot)); // rotate the room 0/90/180/270 degrees

                        GeneratedRooms.Add(Instantiate(RoomTypes[Random.Range(0, RoomTypes.Count)], currentPos, roomRotation, WorldGrid));
                    break;

                    case GenerationState.GeneratingLighting:
                        int lightSpawn = Random.Range(-1, mapBrightness);

                        if (lightSpawn == 0)
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
                    PickSpawnRoom();
                    break;
            }
        }
    }

    private void PickSpawnRoom()
    {
        int roomToReplace = Random.Range(0, GeneratedRooms.Count - mapSizeSqr - 1);
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
    }

    public GameObject spawnRoom;

    public void SpawnPlayer()
    {
        PlayerObject.SetActive(false);

        PlayerObject.transform.position = spawnRoom.transform.position;

        PlayerObject.SetActive(true);
        MainCameraObject.SetActive(false);
    }

    public void NextState()
    {
        currentState++;

        currentPosX = 0;
        currentPosZ = 0;
        currentPosTracker = 0;
        currentPos = Vector3.zero;
    }
}
