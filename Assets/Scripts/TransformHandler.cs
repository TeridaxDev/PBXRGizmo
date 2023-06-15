using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHandler : MonoBehaviour
{
    //This should be 1,0,0; 0,1,0; or 0,0,1
    //Filters the input to just one axis
    [SerializeField] private Vector3 influence;

    public virtual void HandleDragEvent(Vector3 mouseOld, Vector3 mouseNew) { }
}
