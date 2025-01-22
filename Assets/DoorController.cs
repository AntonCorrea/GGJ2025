using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Room Settings")]
    public RoomController currentRoom; // The current room GameObject
    public RoomController nextRoom;    // The next room GameObject

    private bool isTransitioning = false;



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            TransitionToNextRoom();
        }
    }

    private void TransitionToNextRoom()
    {
        isTransitioning = true;

        // Toggle rooms
        if (currentRoom != null)
            //currentRoom.gameObject.SetActive(false);
            currentRoom.FadeRoom();

        if (nextRoom != null)
            nextRoom.gameObject.SetActive(true);

        isTransitioning = false;
    }


}

