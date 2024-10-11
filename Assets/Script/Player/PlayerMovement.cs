using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;         // Speed of the player movement
    private Vector2 movement;                      // Vector2 for movement input
    private Rigidbody2D rb;
    public Animator animator;                      // Animator component for handling animations
    private SpriteRenderer spriteRenderer;         // SpriteRenderer to handle sprite flipping
    private Direction currentDirection = Direction.Up; // Default direction is Up
    private bool isAttacking = false;

    // Direction enum to track the current facing direction
    private enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }

    void Start()
    {
        // Get the Rigidbody2D and SpriteRenderer components attached to the player
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component missing from the Player object!");
            return;
        }

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component missing from the Player object!");
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
        HandleRotation();
        HandleAttack();
    }

    // Handle player movement based on input
    private void HandleMovement()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // Update the Animator parameters for movement
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Determine and update the current direction based on movement input
        UpdateDirection();

        movement.Normalize();
        rb.velocity = movement * moveSpeed;
    }

    // Handle player rotation by flipping the sprite
    private void HandleRotation()
    {
        if (movement.x != 0)
        {
            spriteRenderer.flipX = movement.x < 0;
        }
    }

    // Handle attacking logic based on the current movement direction or facing direction
    private void HandleAttack()
    {
        if (!isAttacking && Input.GetButtonDown("Fire1"))
        {
            StartAttack();
        }
    }

    // Start the attack sequence based on the current direction
    private void StartAttack()
    {
        isAttacking = true;
        // Set the attack direction for the Blend Tree based on the current direction
        switch (currentDirection)
        {
            case Direction.Up:
                animator.SetInteger("Direction", 0); // Assuming 0 is for Up in Blend Tree
                break;
            case Direction.Down:
                animator.SetInteger("Direction", 1); // Assuming 1 is for Down
                break;
            case Direction.Right:
                animator.SetInteger("Direction", 2); // Assuming 2 is for Right
                break;
            case Direction.Left:
                animator.SetInteger("Direction", 3); // Assuming 3 is for Left
                break;
        }

        animator.SetBool("Player_Attack", true); // Trigger attack animation
        StartCoroutine(EndAttack());
    }

    // Coroutine to end the attack after a shorter delay
    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.4f); // Adjust duration as needed
        isAttacking = false; // Reset attack state
        animator.SetBool("Player_Attack", false);
    }

    // Determine and update the current movement direction based on input
    private void UpdateDirection()
    {
        if (movement.y > 0)
        {
            currentDirection = Direction.Up;
        }
        else if (movement.y < 0)
        {
            currentDirection = Direction.Down;
        }
        else if (movement.x > 0)
        {
            currentDirection = Direction.Right;
        }
        else if (movement.x < 0)
        {
            currentDirection = Direction.Left;
        }
    }
}
