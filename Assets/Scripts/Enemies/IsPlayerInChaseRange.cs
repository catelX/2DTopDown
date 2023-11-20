using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class IsPlayerInChaseRange : Node
{
    private Enemy_AI root;
    private float aggroRadius;
    private float chaseAggroRadius;
    private LayerMask playerLayer;
    private bool isChasing;

    public IsPlayerInChaseRange(Enemy_AI _root, float _aggroRadius, float _chaseAggroRadius, LayerMask _playerLayer)
    {
        root = _root;
        aggroRadius = _aggroRadius;
        chaseAggroRadius = _chaseAggroRadius;
        playerLayer = _playerLayer;
        isChasing = false;
    }

    public override NodeState Evaluate()
    {
        RaycastHit2D hit;
        if(isChasing)
        {
            hit = Physics2D.CircleCast(root.GetOrigin(), aggroRadius + chaseAggroRadius, Vector2.zero, 0, playerLayer);
            if(hit.collider == null)
            {
                isChasing = false;
            }
        }
        else
        {
            hit = Physics2D.CircleCast(root.GetOrigin(), aggroRadius, Vector2.zero, 0, playerLayer);
            if (hit.collider != null)
            {
                isChasing = true;
            }
        }
        return hit.collider != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
