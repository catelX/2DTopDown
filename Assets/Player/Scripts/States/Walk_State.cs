using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk_State : State_Base
{
    private Animator anim;
    private Vector2 moveDir;
    public override void EnterState(Direction currentDir)
    {
        switch (currentDir)
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

    public override void Initialize(Animator _anim)
    {
        anim = _anim;
    }

    public override Vector2 StateFixedUpdate()
    {
        return moveDir;
    }

    public override void StateUpdate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            moveDir.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveDir.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir.y = -1;
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            moveDir.x = 0;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            moveDir.y = 0;
        }
        if(moveDir == Vector2.zero)
        {
            ExitState();
        }
    }
}
