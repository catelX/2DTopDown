using System.Collections;
using System.Collections.Generic;
using GridCell;
using UnityEngine;

public abstract class Enemy_Base : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected List<EnemyState_Base> states = new();
    protected Animator anim;
    protected Rigidbody2D rb;

    protected EnemyState_Base currentState;
    [SerializeField] protected Test test;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        foreach (EnemyState_Base state in states)
        {
            state.Initialize(anim, test.GetAStar());
            state.stateExitEvent += ChangeState;
        }
        currentState = states[0];
        currentState.EnterState(test.target);
    }

    public void ExitCurrentState()
    {
        currentState.ExitState();
    }

    public abstract void ChangeState();
}
