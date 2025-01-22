using UnityEngine;

public class RoomController : MonoBehaviour
{
    public FadeInVFX fadevfx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void FadeRoom()
    {
        fadevfx.Fade();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
