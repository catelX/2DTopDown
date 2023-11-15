using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D capsuleCol;
    private Vector2 moveDir;
    [SerializeField] private float moveSpeed;
    private float normalMoveSpeed;
    private Vector2 initialLocalScale;

    private bool takeInput = true;

    public Direction currentDirection;

    private enum State
    {
        Idle,
        Walk,
        Attack,
    }
    private State currentState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCol = GetComponent<CapsuleCollider2D>();
        normalMoveSpeed = moveSpeed;
        initialLocalScale = transform.localScale;
    }

    private void Update()
    {
        if (!takeInput) return;
        SetCurrectDirection();
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            ChangeAnimationToAttack();
            takeInput = false;
            StopPlayerMovement();
            return;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x = -1f;
            ChangeAnimationToWalk();
            FlipHorizontal();
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x = 1f;
            ChangeAnimationToWalk();
            FlipHorizontal();
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveDir.y = 1f;
            ChangeAnimationToWalk();
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir.y = -1f;
            ChangeAnimationToWalk();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed *= 1.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = normalMoveSpeed;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            moveDir.x = 0;
            ChangeAnimationToWalk();
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            moveDir.y = 0;
            ChangeAnimationToWalk();
        }
        if (moveDir == Vector2.zero)
        {
            ChangeAnimationToIdle();
        }
    }

    private void FixedUpdate()
    {
        moveDir = moveDir.normalized;
        rb.velocity = moveSpeed * moveDir;
    }

    private void FlipHorizontal()
    {
        if (currentDirection == Direction.Left && transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(-initialLocalScale.x, initialLocalScale.y);
        }
        else if (currentDirection == Direction.Right && transform.localScale.x < 0)
        {
            transform.localScale = initialLocalScale;
        }
    }

    private void SetCurrectDirection()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentDirection = Direction.Left;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentDirection = Direction.Right;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentDirection = Direction.Up;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentDirection = Direction.Down;
        }
    }

    public void EndAttack_Animation()
    {
        takeInput = true;
    }

    public void StopPlayerMovement()
    {
        moveDir = Vector2.zero;
    }

    private void ChangeAnimationToIdle()
    {
        switch (currentDirection)
        {
            case Direction.Up:
                anim.Play("Idle_Back");
                break;
            case Direction.Down:
                anim.Play("Idle_Front");
                break;
            case Direction.Left:
            case Direction.Right:
                anim.Play("Idle_Side");
                break;
        }
    }
    private void ChangeAnimationToWalk()
    {
        switch (currentDirection)
        {
            case Direction.Up:
                anim.Play("Walk_Back");
                break;
            case Direction.Down:
                anim.Play("Walk_Front");
                break;
            case Direction.Left:
            case Direction.Right:
                anim.Play("Walk_Side");
                break;
        }
    }
    private void ChangeAnimationToAttack()
    {
        switch (currentDirection)
        {
            case Direction.Up:
                anim.Play("Attack_Back");
                break;
            case Direction.Down:
                anim.Play("Attack_Front");
                break;
            case Direction.Left:
            case Direction.Right:
                anim.Play("Attack_Side");
                break;
        }
    }
}
