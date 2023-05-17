using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CCCameraFollower : MonoBehaviour
{

    public GameObject Camera;
    public CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        character.center = new Vector3(Camera.transform.localPosition.x, 1, Camera.transform.localPosition.z);

    }
}
