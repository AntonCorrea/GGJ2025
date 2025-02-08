using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BubbleController : MonoBehaviour
{
    public NpcController currentNPC;
    public BubbleNodeController currentNPCBubbleNode;
    [Header("Nodes")]
    public BubbleNodeController bubbleNodePrefab;
    public BubbleNodeController selectedNode;

    [Header("Bubble Settings")]
    public GameObject bubble;
    public float bubbleRadious;
    public float x, y;

    [Header("Outer Bubble")]
    public float radius = 100f;
    public Vector3 center;
    public List<BubbleNodeController> bubbleList;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float d = Vector3.Distance(new Vector3(x, y, 0f), Input.mousePosition);
            if (d > bubbleRadious)
            {
                CloseBubble();
            }
        }
    }

    public void OpenBubble(NpcController npc)
    {
        currentNPC = npc;

        currentNPC.moveToTarget = false;
        currentNPC.turnToTarget = false;

        center = bubble.transform.position;

        foreach(ActionTarget a in GameManager.Instance.currentRoom.actionTargetList)
        {
            BubbleNodeController b = GameObject.Instantiate(bubbleNodePrefab, bubble.transform);
            b.Init(a);
            b.button.onClick.AddListener(() => OnClickNode(b));
            b.actionTarget = a;
            bubbleList.Add(b);
           
            if(a == currentNPC)
            {
                currentNPCBubbleNode = b;
                b.SetBubble(new Color(.75f, 0f, 0f));
                b.mainNode = true;
            }

            UpdateBubbleNodes(b);
        }

        BubbleNodeController bubbleAux = GameObject.Instantiate(bubbleNodePrefab, bubble.transform);
        bubbleAux.Init(GameManager.Instance.player);
        bubbleAux.button.onClick.AddListener(() => OnClickNode(bubbleAux));
        bubbleAux.actionTarget = GameManager.Instance.player;
        bubbleList.Add(bubbleAux);


        int count = bubbleList.Count;
        // Calculate the angle step for each object
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            // Calculate the angle in radians
            float angle = i * angleStep * Mathf.Deg2Rad;

            // Calculate the new position on the XY plane
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);

            // Set the object's position relative to the center
            bubbleList[i].transform.position = center + newPos;

            // Optionally rotate the object to face the center
            //if (faceCenter)
            //{
            //    Vector3 directionToCenter = center - bubbleList[i].transform.position;
            //    // Using LookRotation, but you might adjust if your forward direction is different
            //    bubbleList[i].transform.rotation = Quaternion.LookRotation(directionToCenter);
            //}
        }
    }

    void CloseBubble()
    {
        currentNPC.moveToTarget = true;
        currentNPC.turnToTarget = true;

        foreach (BubbleNodeController b in bubbleList)
        {
            
            GameObject.Destroy(b.gameObject);
        }
        bubbleList.Clear();
        GameManager.Instance.ClosePowerBubble();
    }

    public void OnClickNode(BubbleNodeController b) // b is the node clicked
    {       
        if (selectedNode == b)
        {
            b.ConnectNode(null);
            selectedNode = null;
            return;
        }

        if (selectedNode && selectedNode.mainNode)
        {
            selectedNode.ConnectNode(b);
            selectedNode = null;
        }
        else
        {
            selectedNode = b;
        }      
    }

    public void UpdateBubbleNodes(BubbleNodeController b)
    {
        if(b.actionTarget == currentNPC.target)
        {
            currentNPCBubbleNode.ConnectNode(b);
        }
    }
}
