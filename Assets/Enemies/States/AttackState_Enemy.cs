using System.Collections;
using System.Collections.Generic;
using GridCell;
using UnityEngine;

public class AttackState_Enemy : EnemyState_Base
{
    Animator anim;
    AStar_Pathfinding aStar;
    GameObject target;
    [SerializeField] private float attackDelay;
    [SerializeField] private GameObject parent;

    public override void EnterState(GameObject target)
    {
        anim.Play("Idle");
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

    float timer;
    public override void StateUpdate()
    {
        timer += Time.deltaTime;
        if(timer >= attackDelay)
        {

        }
    }
}
