using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SortingLayerEcposer : MonoBehaviour
{
    public string SortingLayerName = "Tile";
    public int SortingOrder = 1;

    private void Awake()
    {
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = SortingLayerName;
        gameObject.GetComponent<MeshRenderer>().sortingOrder = SortingOrder;
    }
}
