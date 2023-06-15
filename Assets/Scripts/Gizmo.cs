using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{

    [SerializeField] private List<TransformHandler> gizmoComponents;

    private TransformHandler currentClickedObject;
    Vector3 mousePositionOld;


    void Update()
    {
        //Mouse was just clicked
        if(Input.GetMouseButtonDown(0))
        {
            //Figure out if/which part of the gizmo was clicked on
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < gizmoComponents.Count; i++)
                {
                    if(gizmoComponents[i].gameObject.GetComponent<Collider>() == hit.collider)
                    {
                        currentClickedObject = gizmoComponents[i];

                        //Store the position so we can start handling drag events on the next frame
                        mousePositionOld = Input.mousePosition;
                        break;
                    }
                }
            }
        }
        //Drag Events
        else if(Input.GetMouseButton(0) && currentClickedObject != null)
        {
            currentClickedObject.HandleDragEvent(mousePositionOld, Input.mousePosition);
            mousePositionOld = Input.mousePosition;
        }
        else
        {
            currentClickedObject = null;
        }

    }
}
