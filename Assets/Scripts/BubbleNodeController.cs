using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BubbleNodeController : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public Image image;
    public Button button;

    public BubbleNodeController nodeConected;
    public bool mainNode = false;

    public LineRenderer line;

    public ActionTarget actionTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Init(string name)
    {
        textMesh.text = name;      
    }

    public void SetBubble(Color color)
    {
        image.color = color;
    }

    public void ConnectNode(BubbleNodeController b)
    {
        this.nodeConected = b;
        line.positionCount = 2;
        line.SetPosition(0, this.transform.position); 
        line.SetPosition(1, b.transform.position); 
    }
}
