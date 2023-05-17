using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElevatorBack : MonoBehaviour
{
    public GameObject Player;
    public int moveAwayDistance = 24;

    void Start()
    {
        Player = Camera.main.gameObject; // get ref to plr when hallway is spawning in
    }

    void Update()
    {
        var distance = Vector3.Distance(Player.transform.position, this.transform.position);
        //Debug.Log("distance: " + distance);

        if (distance < moveAwayDistance)
        {
            var elevator = this.gameObject.transform.position;
            var newPos = new Vector3(elevator.x, 0, elevator.z - (moveAwayDistance - distance));
            this.gameObject.transform.position = newPos;
        }
    }
}
