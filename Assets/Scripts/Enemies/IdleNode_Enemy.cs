using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridCell;
using BehaviourTree;

public class IdleNode_Enemy : Node
{
    private Enemy_AI root;
    private Test test;
    private Cell nextCell;

    public IdleNode_Enemy(Enemy_AI _root, Test _test)
    {
        root = _root;
        test = _test;
    }

    private float timer;
    private bool isWaiting;
    public override NodeState Evaluate()
    {
        if(isWaiting)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= 1f)
            {
                isWaiting = false;
                timer = 0;
            }
            else return NodeState.RUNNING;
        }
        if (nextCell == null)
        {
            AStar_Pathfinding aStar = test.GetAStar();
            List<Cell> neighbours = aStar.grid.GetCellNeighbours(aStar.grid.GetCellFromWorldPos(root.transform.position));
            int ranIndex = Random.Range(0, neighbours.Count);
            nextCell = neighbours[ranIndex];
        }

        root.MoveEnemy((nextCell.GetCenterPos() - root.GetOrigin()).normalized);
        if (Vector3.Distance(nextCell.GetCenterPos(), root.GetOrigin()) < 0.2f)
        {
            nextCell = null;
            isWaiting = true;
            root.StopMovement();
        }
        return NodeState.RUNNING;
    }
}
