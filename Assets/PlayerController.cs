using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Forward/backward movement speed
    public float rotationSpeed = 100f; // Rotation speed

    void Update()
    {
        // Get input
        float moveInput = Input.GetAxis("Vertical"); // Forward/backward movement
        float rotationInput = Input.GetAxis("Horizontal"); // Left/right rotation

        // Move the tank forward/backward
        transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);

        // Rotate the tank around its Y-axis
        transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
    }
}

