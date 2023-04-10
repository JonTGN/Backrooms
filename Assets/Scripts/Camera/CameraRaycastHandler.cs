using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycastHandler : MonoBehaviour
{
    Camera plrCam;

    // Start is called before the first frame update
    void Start()
    {
        plrCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(plrCam.transform.position, plrCam.transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "trigger") 
            {
                
            }
            Debug.DrawRay(plrCam.transform.position, plrCam.transform.forward * 100f, Color.green);
        }
    }
}
