using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    public UICanvas uiCanvas;
    public UIWorldSpaceCanvas uiWorldSpaceCanvas;

    public RoomController[] roomArray;
    public RoomController currentRoom;

    public static GameManager Instance { get; private set; }

    public Camera mainCamera; // Assign your main camera
    public CinemachineCamera cinemachineBrain;

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
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        currentRoom = roomArray[0];


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

    public void OpenPowerBubble(NpcController npc)
    {
        player.canMove = false;

        uiCanvas.canvasGroup.blocksRaycasts = false;
        uiCanvas.gameObject.SetActive(false);       

        uiWorldSpaceCanvas.OpenBubble(npc);

        cinemachineBrain.Follow = uiWorldSpaceCanvas.bubbleCenter;


    }

    public void ClosePowerBubble()
    {
        player.canMove = true;

        uiCanvas.canvasGroup.blocksRaycasts = true;
        uiCanvas.gameObject.SetActive(true);

        uiWorldSpaceCanvas.CloseBubble();

        cinemachineBrain.Follow = player.transform;
    }



    void GameOver()
    {
        player.gameObject.SetActive(false);
        uiCanvas.FadeToBlack();
    }

    public void ResetButton()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        //foreach(RoomController room in roomArray)
        //{
        //    room.ResetRoom();
        //}
        currentRoom.ResetRoom();

        foreach(var a in currentRoom.actionTargetList)
        {
            if(a is NpcController npc)
            {
                npc.ResetNPC();
            }
        }

        player.transform.position = currentRoom.playerSpawn.position;
        player.transform.rotation = currentRoom.playerSpawn.rotation;
        player.gameObject.SetActive(true);
        player.ResetPlayer();

        uiCanvas.ResetUI();
    }
}
