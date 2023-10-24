using System.Collections;
using System.Collections.Generic;
using GridCell;
using UnityEngine;

public class AlertState_Enemy : EnemyState_Base
{
    private Animator anim;
    private AStar_Pathfinding aStar;
    private GameObject target;
    [SerializeField] private float duration;
    [SerializeField] private GameObject parent;

    public override void EnterState(GameObject target)
    {
        anim.Play("Alert");
        timer = 0;
        this.target = target;
    }

    public override void Initialize(Animator anim, AStar_Pathfinding aStar)
    {
        this.anim = anim;
        this.aStar = aStar;
    }

    public override Vector2 StateFixedUpdate()
    {
        return Vector2.zero;
    }

    private float timer;
    public override void StateUpdate()
    {
        if (duration != 0)
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
