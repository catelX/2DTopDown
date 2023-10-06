using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Base : MonoBehaviour
{
    [SerializeField] protected List<EnemyState_Base> states = new();
    protected Animator anim;
    protected Rigidbody2D rb;

    protected EnemyState_Base currentState;
    public GameObject nextDesti;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        foreach (EnemyState_Base state in states)
        {
            state.Initialize(anim);
            state.stateExitEvent += ChangeState;
        }
        currentState = states[0];
        currentState.EnterState(nextDesti.transform.position);
    }

    private void Update()
    {
        currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        rb.velocity = 5 * currentState.StateFixedUpdate();
    }

    private void ChangeState()
    {
        switch (currentState.ID())
        {
            case "Idle":
                currentState = states[1];
                currentState.EnterState(nextDesti.transform.position);
                break;
            case "Move":
                currentState = states[0];
                currentState.EnterState(nextDesti.transform.position);
                break;
        }
    }
}
