using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public UICanvas ui;

    public RoomController[] rooms;
    public RoomController currentRoom;

    public List<NpcController> NPCList;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentRoom = rooms[0];


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHit(int dmg)
    {
        player.GetHit(dmg);

        if(player.lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        player.gameObject.SetActive(false);
        ui.FadeToBlack();
    }

    public void ResetButton()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        foreach(RoomController room in rooms)
        {
            room.ResetRoom();
        }

        foreach(NpcController npc in NPCList)
        {
            npc.ResetNPC();
        }

        player.transform.position = currentRoom.playerSpawn.position;
        player.transform.rotation = currentRoom.playerSpawn.rotation;
        player.gameObject.SetActive(true);
        player.ResetPlayer();

        ui.ResetUI();
    }
}
