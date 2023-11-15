using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class State_Base : MonoBehaviour
{
    [SerializeField] protected string stateID;
    public event Action stateExitEvent;
    public abstract void Initialize(Animator anim);

    public virtual void ExitState()
    {
        stateExitEvent();
    }

    public string ID()
    {
        return stateID;
    }

    public abstract Vector2 StateFixedUpdate();
    public abstract void StateUpdate();

    public abstract void EnterState(Direction currentDir);
}
