using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerAngle : MonoBehaviour
{
    Camera plrCam;
    public float angle;
    public bool shouldCheckForAngle;

    void Start()
    {
        plrCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldCheckForAngle)
        {
            Vector3 forward = plrCam.transform.forward;
            forward.y = 0; // only want dir in x/z plane
            angle = Quaternion.LookRotation(forward).eulerAngles.y;

            //if (angle > 180f) angle -= 360f;

            //Debug.Log("angle: " + angle);
        }
        

        
    }
}
