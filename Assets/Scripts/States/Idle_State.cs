using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_State : State_Base
{
    private Animator anim;
    public override void EnterState(Direction currentDir)
    {
        switch (currentDir)
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

    public override void Initialize(Animator _anim)
    {
        anim = _anim;
    }

    public override Vector2 StateFixedUpdate()
    {
        return Vector2.zero;
    }

    public override void StateUpdate()
    {
        return;
    }
}
