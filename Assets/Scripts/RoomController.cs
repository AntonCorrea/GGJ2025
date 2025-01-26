using UnityEngine;

public class RoomController : MonoBehaviour
{
    public FadeInVFX fadevfx;

    public Transform playerSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void FadeRoom()
    {
        fadevfx.Fade();
    }

    public void ResetRoom()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
