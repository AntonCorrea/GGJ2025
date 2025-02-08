using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BubbleNodeController : MonoBehaviour
{
    public ActionTarget actionTarget;
    public BubbleNodeController nodeConected;
    public bool mainNode = false;

    public TextMeshProUGUI textMesh;
    public Image image;
    public Button button;
    public LineRenderer line;

    public void Init(ActionTarget actionTarget)
    {
        textMesh.text = actionTarget.gameObject.name;      
    }

    public void SetBubble(Color color)
    {
        image.color = color;
    }

    public void ConnectNode(BubbleNodeController b)
    {
        if (b)
        {
            this.nodeConected = b;
            //(actionTarget as NpcController).target = b.actionTarget;
            (actionTarget as NpcController).SetTarget(b.actionTarget);

            line.positionCount = 2;
            line.SetPosition(0, this.transform.position);
            line.SetPosition(1, this.nodeConected.transform.position);
        }
        else
        {
            this.nodeConected = null;
            line.positionCount = 0;
        }
        
    }

    private void Update()
    {
        if(line.positionCount > 1)
        {
            line.SetPosition(0, this.transform.position);
            line.SetPosition(1, this.nodeConected.transform.position);
        }
    }
}
