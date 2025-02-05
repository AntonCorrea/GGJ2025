using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Transform playerSpawn;
    public List<NpcController> npcsList;
    public List<WorkStationController> worksList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Start()
    {
        foreach(NpcController npc in GetComponentsInChildren<NpcController>())
        {
            npcsList.Add(npc);
        }

        foreach(WorkStationController work in GetComponentsInChildren<WorkStationController>())
        {
            worksList.Add(work);
        }
    }
    public void ResetRoom()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
