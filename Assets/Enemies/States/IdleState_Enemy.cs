using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState_Enemy : EnemyState_Base
{
    private Animator anim;
    [SerializeField] private float duration;

    public override void EnterState(Vector2 destination)
    {
        anim.Play("Idle");
        timer = 0;
    }

    public override void Initialize(Animator _anim)
    {
        anim = _anim;
    }

    public override Vector2 StateFixedUpdate()
    {
        return Vector2.zero;
    }

    private float timer;
    public override void StateUpdate()
    {
        if(duration != 0)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                timer = 0;
                ExitState();
            }
        }
    }
}
