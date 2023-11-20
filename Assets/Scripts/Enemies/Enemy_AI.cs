using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridCell;

public class Enemy_AI : MonoBehaviour
{

    [SerializeField] protected float moveSpeed;
    protected Animator anim;
    protected Rigidbody2D rb;

    [SerializeField] protected Test test;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public Vector3 GetOrigin()
    {
        return transform.position;
    }

    public void MoveEnemy(Vector3 moveDir)
    {
        rb.velocity = moveSpeed * moveDir;
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }
}
