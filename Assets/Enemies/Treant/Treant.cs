using System.Collections;
using System.Collections.Generic;
using GridCell;
using UnityEngine;

public class Treant : Enemy_Base
{
    private void Update()
    {
        currentState.StateUpdate();
        if(currentState == states[1])
        {
            ScanDamageable();
        }
        AggroAreaScan();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveSpeed * currentState.StateFixedUpdate();
    }

    public override void ChangeState()
    {
        switch (currentState.ID())
        {
            case "Idle":
                currentState = states[1];
                currentState.EnterState(test.target);
                break;
            case "Alert":
                currentState = states[1];
                currentState.EnterState(test.target);
                break;
            case "Move":
                currentState = states[0];
                currentState.EnterState(test.target);
                break;
        }
    }

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackRadius;
    [SerializeField] private float aggroRadius;
    private float chaseAggroRadius = 2f;
    private void ScanDamageable()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, attackRadius, Vector2.zero, 0, playerLayer);
        if(hit.collider != null)
        {
            //change to attack state
        }
    }

    private void AggroAreaScan()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, aggroRadius + chaseAggroRadius, Vector2.zero, 0, playerLayer);
        if(hit.collider != null)
        {
            AStar_Pathfinding aStar = test.GetAStar();
            List<Cell> path = aStar.ReturnPath(aStar.grid.GetCellFromWorldPos(transform.position), aStar.grid.GetCellFromWorldPos(test.target.transform.position));
            if(path.Count != 0 && currentState == states[0])
            {
                chaseAggroRadius = 5f;
                currentState = states[2];
                currentState.EnterState(test.target);
            }
            else if(path.Count == 0)
            {
                chaseAggroRadius = 0;
                currentState = states[0];
                currentState.EnterState(test.target);
            }
        }
        else if(currentState != states[0])
        {
            chaseAggroRadius = 0;
            currentState = states[0];
            currentState.EnterState(test.target);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0,255,0,0.5f);
        Gizmos.DrawSphere(transform.position, aggroRadius + chaseAggroRadius);
        Gizmos.color = new Color(255, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, attackRadius);
    }
}
