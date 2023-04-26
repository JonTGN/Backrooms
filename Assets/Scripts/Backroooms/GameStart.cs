using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GenerationManager genMngr;
    public Transform WorldGrid;
    public GameObject player;
    public GameObject oldCamera;
    public GameObject canvas;

    void Start()
    {
        genMngr.WorldGrid = WorldGrid;
        genMngr.GenerateWorld(true); // gen chunk w/ spawn room in it

        player.SetActive(true);
        oldCamera.SetActive(false);
        canvas.SetActive(false);
    }
}
