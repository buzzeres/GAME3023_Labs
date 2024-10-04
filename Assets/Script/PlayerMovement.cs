using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;  // Speed of the player movement

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    // Handle player movement based on input, moving directly without physics interactions
    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; 
        float y = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;  
        transform.Translate(x, y, 0);  // Move player directly without physics
    }
}
