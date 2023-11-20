using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridCell;
using BehaviourTree;

public class ChaseNode_Enemy : Node
{
    private Enemy_AI root;
    private Test test;
    private Cell nextCell;

    public ChaseNode_Enemy(Enemy_AI _root, Test _test)
    {
        root = _root;
        test = _test;
    }

    private float reEvaluatePathTimer;

    public override NodeState Evaluate()
    {
        reEvaluatePathTimer += Time.fixedDeltaTime;

        if (nextCell == null || reEvaluatePathTimer >= 1f)
        {
            reEvaluatePathTimer = 0;
            AStar_Pathfinding aStar = test.GetAStar();
            List<Cell> path = aStar.ReturnPath(aStar.grid.GetCellFromWorldPos(root.GetOrigin()), aStar.grid.GetCellFromWorldPos(test.target.transform.position));
            nextCell = path[^1];
        }

        root.MoveEnemy((nextCell.GetCenterPos() - root.GetOrigin()).normalized);
        if(Vector3.Distance(nextCell.GetCenterPos(), root.GetOrigin()) < 0.2f)
        {
            nextCell = null;
        }
        return NodeState.RUNNING;
    }
}
