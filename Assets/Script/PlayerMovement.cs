using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;  // Speed of the player movement
    private Rigidbody2D rb;
    public int[] intarray = new int[4];

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component missing from the Player object!");
            return;
        }

      

        // Set Rigidbody2D properties
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        HandleMovement();
    }

    // Handle player movement based on input
    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(x, y);
        Vector2 velocity = inputVector * moveSpeed;

        rb.velocity = velocity;
    }
}
