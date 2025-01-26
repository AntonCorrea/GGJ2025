using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsController : MonoBehaviour
{

    public List<Transform> waypointList;
    // Start is called before the first frame update
    void Awake()
    {
        //waypoints = new List<Transform>(transform.GetComponentsInChildren<Transform>());
        //waypoints.Remove(transform);
        foreach (Transform t in transform)
        {
            waypointList.Add(t);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        int childs = transform.childCount - 1;
        for (int i = 0; i < childs; i++)
        {
            transform.GetChild(i).name = "Waypoint_" + i.ToString();
            Gizmos.DrawWireSphere(transform.GetChild(i).position, 1f);
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        transform.GetChild(childs).name = "Waypoint_" + childs.ToString();
        Gizmos.DrawWireSphere(transform.GetChild(childs).position, 1f);
        Gizmos.DrawLine(transform.GetChild(childs).position, transform.GetChild(0).position);

    }
}