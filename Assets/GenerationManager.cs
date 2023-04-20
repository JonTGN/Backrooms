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
                        GeneratedRooms.Add(Instantiate(RoomTypes[Random.Range(0, RoomTypes.Count)], currentPos, Quaternion.identity, WorldGrid));
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
                    int roomToReplace = Random.Range(0, GeneratedRooms.Count);

                    spawnRoom = Instantiate(SpawnRoom, GeneratedRooms[roomToReplace].transform.position, Quaternion.identity, WorldGrid);

                    Destroy(GeneratedRooms[roomToReplace]);

                    GeneratedRooms[roomToReplace] = spawnRoom;

                    break;
            }
        }
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
