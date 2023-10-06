using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState_Enemy : EnemyState_Base
{
    private Animator anim;
    private Vector2 moveDir;
    private Vector2 startPos;
    private Vector2 destination;

    public override void EnterState(Vector2 _destination)
    {
        startPos = transform.position;
        destination = _destination;
        Vector2 distance = destination - startPos;
        moveDir = distance / 10;
        if (Vector2.Distance(destination, transform.position) <= 0.5f)
        {
            ExitState();
        }
        else anim.Play("Move");
    }

    public override void Initialize(Animator _anim)
    {
        anim = _anim;
    }

    public override Vector2 StateFixedUpdate()
    {
        return moveDir.normalized;
    }

    public override void StateUpdate()
    {
        if(Vector2.Distance(destination,(Vector2)transform.position) < 0.2f)
        {
            ExitState();
        }
    }
}
