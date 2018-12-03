using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(TextMesh))]
public class SortingLayers : MonoBehaviour
{
    public string sortingLayerName;
    public int sortingOrder;


    /// <summary>
    /// only runs update if UNITY_EDITOR = true
    /// in this case, while editor is open and not in play mode
    /// </summary>
#if UNITY_EDITOR
    public void Update()
    {
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = sortingLayerName;
        gameObject.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
    }
#endif
}
