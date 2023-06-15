using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHandler : MonoBehaviour
{
    //This should be 1,0,0; 0,1,0; or 0,0,1
    //Filters the input to just one axis
    [SerializeField] protected Vector3 influence;
    //Reference to the gizmo's origin point
    [SerializeField] protected Transform gizmoOrigin;

    public const float PRECISION_THRESHOLD = 0.001f;

    //I found this helper function here: https://github.com/pshtif/RuntimeTransformHandle (MIT liscense)
    //it finds the point on vector AB (ray) that is closest in distance to any point on ray CD (other), and returns the distance between that point and A
    //it's crucial for aligning the end user's mouse movements to the axis they are trying to manipulat in world space
    public static float ClosestPointOnRay(Ray ray, Ray other)
    {
        // based on: https://math.stackexchange.com/questions/1036959/midpoint-of-the-shortest-distance-between-2-rays-in-3d
        // note: directions of both rays must be normalized
        // ray.origin -> a
        // ray.direction -> b
        // other.origin -> c
        // other.direction -> d

        float bd = Vector3.Dot(ray.direction, other.direction);
        float cd = Vector3.Dot(other.origin, other.direction);
        float ad = Vector3.Dot(ray.origin, other.direction);
        float bc = Vector3.Dot(ray.direction, other.origin);
        float ab = Vector3.Dot(ray.origin, ray.direction);

        float bottom = bd * bd - 1f;
        if (Mathf.Abs(bottom) < PRECISION_THRESHOLD)
        {
            return 0;
        }

        float top = ab - bc + bd * (cd - ad);
        return top / bottom;
    }

    public virtual void HandleDragEvent(Vector3 mouseOld, Vector3 mouseNew) { }
}
