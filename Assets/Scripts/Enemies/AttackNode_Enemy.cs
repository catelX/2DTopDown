using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class AttackNode_Enemy : Node
{
    private Enemy_AI root;
    private float attackRadius;
    private LayerMask playerLayer;
    private bool isAttacking;

    public AttackNode_Enemy(Enemy_AI _root, float _attackRadius, LayerMask _playerLayer)
    {
        root = _root;
        attackRadius = _attackRadius;
        playerLayer = _playerLayer;
    }

    public override NodeState Evaluate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(root.GetOrigin(), attackRadius, Vector2.zero, 0, playerLayer);
        if(!isAttacking)
        {
            if(hit.collider == null)
            {
                return NodeState.FAILURE;
            }
            else
            {
                isAttacking = true;
                return NodeState.RUNNING;
            }
        }
        else
        {
            Debug.Log("Attack");
            isAttacking = false;
            return NodeState.SUCCESS;
        }
    }
}
