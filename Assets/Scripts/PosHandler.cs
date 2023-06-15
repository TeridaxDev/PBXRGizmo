using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosHandler : TransformHandler
{
    public override void HandleDragEvent(Vector3 mouseOld, Vector3 mouseNew)
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(mouseNew);

        //The offset required to make the positionhandler track the mouse, instead of the gizmo's origin
        Vector3 originGizmoDistance = gizmoOrigin.position - this.transform.position;
        //The direction this handler is facing; most likely one of the world axes
        Vector3 worldPositive = (this.transform.position - gizmoOrigin.position).normalized;
        //ray from the gizmo's origin to this handler's axis
        Ray positiveRay = new Ray(gizmoOrigin.transform.position, worldPositive);
        //finds the shortest distance from the mouse postion to the above ray, then returns the distance between that point and the ray's origin
        float closestT = ClosestPointOnRay(positiveRay, cameraRay);
        //the amount of distance to translate
        Vector3 hitPoint = positiveRay.GetPoint(closestT);

        //offsets the destination position by the distance between this object and the center of the gizmo
        Vector3 offset = hitPoint + originGizmoDistance - gizmoOrigin.position;

        Vector3 position = gizmoOrigin.position + offset;

        gizmoOrigin.parent.position = position;

    }
}
