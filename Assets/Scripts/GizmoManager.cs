using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoManager : MonoBehaviour
{
    [SerializeField] private Gizmo gizmo;

    private GameObject[] objects;
    private Collider currentlySelected;

    //The goal of this class is to move the gizmo to whatever object the user clicks on

    private void Start()
    {
        objects = GameObject.FindGameObjectsWithTag("Transformable");
    }

    private void Update()
    {
        //Mouse was just clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Figure out if/which part of the gizmo was clicked on
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.CompareTag("Transformable"))
                {
                    //Turn off the collider of the object that was clicked on, so it doesn't interfere with clicking on the gizmo
                    //Also re-enable the previously selected object
                    if (currentlySelected != null)
                        currentlySelected.enabled = true;
                    currentlySelected = hit.collider;
                    currentlySelected.enabled = false;

                    //Move the gizmo into position
                    gizmo.transform.parent = hit.transform;
                    gizmo.transform.position = hit.transform.position;
                    gizmo.transform.rotation = hit.transform.rotation;
                    gizmo.transform.localScale = Vector3.one;
                    gizmo.transform.localScale = new Vector3(1 / gizmo.transform.lossyScale.x, 1 / gizmo.transform.lossyScale.y, 1 / gizmo.transform.lossyScale.z);

                    gizmo.gameObject.SetActive(true);
                }
            }
        }
    }

}
