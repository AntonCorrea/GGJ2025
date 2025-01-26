using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Destroy(this.gameObject);
    }
}
