using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f; // Speed of player movement
    private Vector2 movement;                      // Stores movement input
    private Rigidbody2D rb;                        // Rigidbody2D component for physics
    public Animator animator;                      // Animator for handling animations
    private SpriteRenderer spriteRenderer;         // SpriteRenderer to handle sprite flipping
    private Direction currentDirection = Direction.Up; // Default direction facing up
    private bool isAttacking = false;              // Flag to check if player is attacking
    private bool canMove = true;                   // Flag to control if the player can move
    public event Action isEncountered;             // Event triggered when an encounter is detected

    // Enum to track player's current facing direction
    private enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }

    private void Start()
    {
        // Get the Rigidbody2D and SpriteRenderer components attached to the player
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure components are present to avoid null reference errors
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

    public void HandleUpdate()
    {
        // Only allow movement and animations if canMove is true
        if (canMove)
        {
            HandleMovement();
            HandleRotation();
            HandleAttack();
            CheckForEncounters();
        }
        else
        {
            // Stop all movement and reset animations if movement is disabled
            rb.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0); // Set speed to zero for idle animation
        }
    }

    // Handle player movement based on input
    private void HandleMovement()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // Update animator parameters for movement
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Update the current direction based on movement input
        UpdateDirection();

        // Normalize movement vector and apply it to the Rigidbody2D
        movement.Normalize();
        rb.velocity = movement * moveSpeed;
    }

    // Handle sprite flipping based on horizontal movement
    private void HandleRotation()
    {
        if (movement.x != 0)
        {
            spriteRenderer.flipX = movement.x < 0; // Flip sprite based on movement direction
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
                animator.SetInteger("Direction", 0);
                break;
            case Direction.Down:
                animator.SetInteger("Direction", 1);
                break;
            case Direction.Right:
                animator.SetInteger("Direction", 2);
                break;
            case Direction.Left:
                animator.SetInteger("Direction", 3);
                break;
        }

        animator.SetBool("Player_Attack", true);
        StartCoroutine(EndAttack());
    }

    // Coroutine to end the attack after a short delay
    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.4f); // Adjust duration as needed
        isAttacking = false;
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

    // Check for encounters and trigger the encounter event if conditions are met
    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Encounter")) != null)
        {
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                isEncountered?.Invoke(); // Trigger the encounter event
            }
        }
    }

    // Disable player movement and animations (to be called from GameController when battle starts)
    public void DisableMovement()
    {
        canMove = false;
        rb.velocity = Vector2.zero; // Immediately stop movement
        animator.SetFloat("Speed", 0); // Set speed to zero for idle animation
    }

    // Enable player movement and animations (to be called from GameController when battle ends)
    public void EnableMovement()
    {
        canMove = true;
    }
}
