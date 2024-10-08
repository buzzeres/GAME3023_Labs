using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;         // Speed of the player movement
    [SerializeField] float attackCooldown = 0f;  // Cooldown time for attacks
    private float lastAttackTime;                  // Time of the last attack
    private Vector2 movement;                      // Vector2 for movement input
    private Rigidbody2D rb;
    public Animator animator;                      // Animator component for handling animations
    private SpriteRenderer spriteRenderer;         // SpriteRenderer to handle sprite flipping
    private Direction currentDirection = Direction.Up; // Default direction is Up

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
        // Check for attack input, cooldown, and ensure the player is moving before attacking
        if (Input.GetButtonDown("Fire1") && Time.time >= lastAttackTime + attackCooldown && IsPlayerMoving())
        {
            StartAttack();
            lastAttackTime = Time.time; // Update last attack time
        }
    }

    // Start the attack sequence based on the current direction
    private void StartAttack()
    {
        animator.SetBool("IsAttacking", true); // Trigger attack in Animator

        // Set the attackDirection parameter based on the current direction enum
        switch (currentDirection)
        {
            case Direction.Up:
                animator.SetInteger("attackDirection", 0); // Up
                break;
            case Direction.Down:
                animator.SetInteger("attackDirection", 1); // Down
                break;
            case Direction.Right:
                animator.SetInteger("attackDirection", 2); // Right
                break;
            case Direction.Left:
                animator.SetInteger("attackDirection", 3); // Left
                break;
        }

        // Play a generic attack animation
        animator.Play("Player_Attack");

        // Ensure attack ends after the cooldown period
        StartCoroutine(EndAttack());
    }

    // Coroutine to end the attack after a brief delay
    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(attackCooldown); // Wait for the cooldown duration
        animator.SetBool("IsAttacking", false);          // Reset attack state in Animator
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

    // Check if the player is currently moving in any direction
    private bool IsPlayerMoving()
    {
        return movement.sqrMagnitude > 0;
    }
}
