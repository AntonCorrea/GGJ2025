using UnityEngine;

public abstract class ActionTarget:MonoBehaviour
{
    [Header("Navigation")]
    public WaypointsController waypoints;
    public Transform targetTransform;

}
