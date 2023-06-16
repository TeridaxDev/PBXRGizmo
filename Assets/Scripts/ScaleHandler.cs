using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHandler : TransformHandler
{
    float sensitivity = 0.04f;

    public override void HandleDragEvent(Vector3 mouseOld, Vector3 mouseNew)
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(mouseNew);

        //This is the same as the position implementation
        Vector3 worldPositive = (this.transform.position - gizmoOrigin.position).normalized;
        Ray positiveRay = new Ray(gizmoOrigin.transform.position, worldPositive);
        float closestT = ClosestPointOnRay(positiveRay, cameraRay);
        Vector3 hitPoint = positiveRay.GetPoint(closestT);

        //Unlike position, the gizmo (shouldn't) scale with the object it's transforming, which means we aren't trying to 0 out the distance between this object and the mouse
        //instead, use distance between mouseold and mousenew
        float distance = Vector3.Distance(gizmoOrigin.parent.position, hitPoint);
        float scaleDelta = distance / (mouseNew - mouseOld).magnitude - 1f;
        scaleDelta *= sensitivity;


        Vector3 scale = Vector3.Scale(gizmoOrigin.parent.localScale, influence * scaleDelta + Vector3.one);
        if(!float.IsNaN(scale.magnitude))
            gizmoOrigin.parent.localScale = scale;

        //Fix the gizmo's scale so it doesn't warp
        gizmoOrigin.localScale = Vector3.one;
        gizmoOrigin.localScale = new Vector3(1 / gizmoOrigin.lossyScale.x, 1 / gizmoOrigin.lossyScale.y, 1 / gizmoOrigin.lossyScale.z);

    }
}
