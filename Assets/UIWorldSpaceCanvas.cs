using UnityEngine;

public class UIWorldSpaceCanvas : MonoBehaviour
{
    public Transform bubbleOrigin;
    public Transform bubbleCenter;

    public BubbleController bubbleController;

    public CanvasGroup canvasGroup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OpenBubble(NpcController npc)
    {
        bubbleController.gameObject.SetActive(true);

        bubbleController.OpenBubble(npc);

        bubbleOrigin.position = npc.transform.position;
    }

    public void CloseBubble()
    {
        bubbleController.gameObject.SetActive(false);
    }
}
