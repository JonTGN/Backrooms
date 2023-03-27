using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerTrigger : MonoBehaviour
{
    public CornerTrigger otherCorner;
    public GameObject go1;
    public GameObject go2;
    public GameObject player;

    public bool isX;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (isX)
            {
                if (player.transform.position.x < gameObject.transform.position.x)
                {
                    go1.SetActive(true);
                    go2.SetActive(false);
                }
            }

            else // is on z
            {
                if (player.transform.position.z < gameObject.transform.position.z)
                {
                    go1.SetActive(true);
                    go2.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isX)
            {
                if (player.transform.position.x < gameObject.transform.position.x)
                {
                    go1.SetActive(false);
                    go2.SetActive(true);
                }
            }

            else // is on z
            {
                if (player.transform.position.z < gameObject.transform.position.z)
                {
                    go1.SetActive(false);
                    go2.SetActive(true);
                }
            }
        }
    }
}
