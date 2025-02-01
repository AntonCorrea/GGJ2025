using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public UICanvas ui;
    public Transform bubbleCameraPoint;

    public RoomController[] rooms;
    public RoomController currentRoom;

    public List<NpcController> NPCList;
    public static GameManager Instance { get; private set; }

    public Camera mainCamera; // Assign your main camera

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

    public void WorldToScreen(NpcController npc)
    {
        //if (worldObject == null || uiElement == null || mainCamera == null) return;

        // Step 1: Convert world position to screen position
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(npc.transform.position);

        // Step 2: Convert screen position to UI local position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            ui.canvas.GetComponent<RectTransform>(), // The parent UI element (Canvas)
            screenPoint,
            ui.canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, // Camera (if needed)
            out Vector2 uiPosition
        );

        // Step 3: Apply the position to the UI element
        ui.bubbleOrigin.anchoredPosition = uiPosition;

        Vector3 worldPoint = ConvertUIToWorld(ui.bubbleCenter);
        bubbleCameraPoint.position = worldPoint;
    }

    Vector3 ConvertUIToWorld(RectTransform uiElement)
    {
        // Get the UI element’s screen position
        Vector3 screenPos = uiElement.position;

        // Set a custom depth since Screen Space - Overlay doesn’t have a real depth
        screenPos.z = 30f;

        // Convert to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);
        return worldPosition;
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
