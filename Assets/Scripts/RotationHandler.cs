using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : TransformHandler
{
    public override void HandleDragEvent(Vector3 mouseOld, Vector3 mouseNew)
    {
        Ray cameraRayOld = Camera.main.ScreenPointToRay(mouseOld);
        Ray cameraRayNew = Camera.main.ScreenPointToRay(mouseNew);

        //This plane is best visualized as the plane intersected by the arc the user clicks on
        Plane axisPlane = new Plane(influence, gizmoOrigin.position);

        //Find the point that the user clicked on the plane last frame
        Vector3 startHitPoint;
        float hit;
        if (axisPlane.Raycast(cameraRayOld, out hit))
        {
            startHitPoint = cameraRayOld.GetPoint(hit);
        }
        else
        {
            startHitPoint = axisPlane.ClosestPointOnPlane(Camera.main.ScreenToWorldPoint(mouseOld));
        }

        //tangent and bitangent of the point being clicked on, vs the plane of rotation
        Vector3 tangent = (startHitPoint - gizmoOrigin.parent.position).normalized;
        Vector3 biTangent = Vector3.Cross(influence, tangent);

        //if the user is somehow not clicking on the plane at all anymore, bail out
        if (!axisPlane.Raycast(cameraRayNew, out hit))
        {
            return;
        }


        Quaternion startRotation = gizmoOrigin.parent.rotation;

        //Position the user is currently clicking on
        Vector3 hitPoint = cameraRayNew.GetPoint(hit);
        //the direction of the clicked point from the gizmo origin
        Vector3 hitDirection = (hitPoint - gizmoOrigin.parent.position).normalized;
        //assuming the points the user is clicking on and were clicking on are two vectors whose origin is the gizmo's origin,
        //find the angle between the two vectors
        float x = Vector3.Dot(hitDirection, tangent);
        float y = Vector3.Dot(hitDirection, biTangent);
        float angleRadians = Mathf.Atan2(y, x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        gizmoOrigin.parent.localRotation = startRotation * Quaternion.AngleAxis(angleDegrees, influence);

    }
        
}
