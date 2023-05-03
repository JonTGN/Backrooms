using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("in hand");
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
