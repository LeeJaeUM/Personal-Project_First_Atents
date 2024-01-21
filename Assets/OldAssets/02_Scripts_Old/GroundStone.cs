using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundStone : MonoBehaviour
{
    public float gizLine = 2.0f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(gizLine, gizLine, 0));
    }
}
