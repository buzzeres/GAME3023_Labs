using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    private Vector2 movement;
    private Rigidbody2D rb;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private Direction currentDirection = Direction.Up;
    private bool isAttacking = false;
    private bool canMove = true;
    public event Action isEncountered;

    private enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }

    private void Start()
    {
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

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void HandleUpdate()
    {
        if (canMove)
        {
            HandleMovement();
            HandleRotation();
            HandleAttack();
            CheckForEncounters();
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0);
        }
    }

    private void HandleMovement()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        UpdateDirection();

        movement.Normalize();
        rb.velocity = movement * moveSpeed;
    }

    private void HandleRotation()
    {
        if (movement.x != 0)
        {
            spriteRenderer.flipX = movement.x < 0;
        }
    }

    private void HandleAttack()
    {
        if (!isAttacking && Input.GetButtonDown("Fire1"))
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        isAttacking = true;

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

    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
        animator.SetBool("Player_Attack", false);
    }

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

    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Encounter")) != null)
        {
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                isEncountered?.Invoke();
            }
        }
    }

    public void DisableMovement()
    {
        canMove = false;
        rb.velocity = Vector2.zero;
        animator.SetFloat("Speed", 0);
    }

    public void EnableMovement()
    {
        canMove = true;
    }
}
