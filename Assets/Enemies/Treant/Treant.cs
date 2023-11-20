using System.Collections;
using System.Collections.Generic;
using GridCell;
using BehaviourTree;
using UnityEngine;

public class Treant : Enemy_AI
{

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackRadius;
    [SerializeField] private float aggroRadius;
    private float chaseAggroRadius = 2f;

    private Node topNode;

    private void Start()
    {
        ConstructBehaviourTree();
    }

    private void FixedUpdate()
    {
        topNode.Evaluate();
    }

    private void ConstructBehaviourTree()
    {
        AttackNode_Enemy attackNode = new AttackNode_Enemy(this, attackRadius, playerLayer);
        IsPlayerInChaseRange isPlayerInChaseRange = new IsPlayerInChaseRange(this, aggroRadius, chaseAggroRadius, playerLayer);
        ChaseNode_Enemy chaseNode = new ChaseNode_Enemy(this, test);
        Sequence chasePlayer = new Sequence(new List<Node> { isPlayerInChaseRange, chaseNode });
        IdleNode_Enemy idleNode = new IdleNode_Enemy(this, test);
        topNode = new Selector(new List<Node> { attackNode, chasePlayer, idleNode });
    }
}
