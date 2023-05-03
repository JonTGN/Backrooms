using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOffGameObject : MonoBehaviour
{
    public GameObject gameObjectToSet;
    public bool activate;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            gameObjectToSet.SetActive(activate);
        }
    }
}
