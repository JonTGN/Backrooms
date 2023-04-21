using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSphere : MonoBehaviour
{
    public float lightRenderDistance;
    public float chunkRenderDistance;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lightRenderDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chunkRenderDistance);
    }
}
