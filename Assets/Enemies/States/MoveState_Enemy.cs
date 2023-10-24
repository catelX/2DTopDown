using System.Collections;
using System.Collections.Generic;
using System.Threading;
using GridCell;
using UnityEngine;

public class MoveState_Enemy : EnemyState_Base
{
    private Animator anim;
    private AStar_Pathfinding aStar;
    private GameObject target;
    private List<Cell> path = new();
    private Cell nextCell = null;
    private Vector2 moveDir;


    public GameObject parent;

    public override void EnterState(GameObject target)
    {
        StartPathFinding(target.transform.position);
        this.target = target;
        anim.Play("Move");
    }

    public override void Initialize(Animator anim, AStar_Pathfinding aStar)
    {
        this.anim = anim;
        this.aStar = aStar;
    }

    public override Vector2 StateFixedUpdate()
    {
        return moveDir.normalized;
    }

    public override void StateUpdate()
    {
        PathFindInterval();
        if (Vector3.Distance(nextCell.GetCenterPos(), parent.transform.position) < 0.2f)
        {
            parent.transform.position = nextCell.GetCenterPos();
            if(path.Count != 0)nextCell = path[^1];
            path.Remove(nextCell);
        }
        moveDir = nextCell.GetCenterPos() - parent.transform.position;
    }

    public void StartPathFinding(Vector3 targetPos)
    {
        if(aStar.grid.GetCellFromWorldPos(targetPos) == null)
        {
            ExitState();
            return;
        }
        path = aStar.ReturnPath(aStar.grid.GetCellFromWorldPos(parent.transform.position), aStar.grid.GetCellFromWorldPos(targetPos));
        if(path.Count != 0)
        {
            Test.Instance.SetPath(path, aStar.grid.GetCellFromWorldPos(parent.transform.position));
            nextCell = path[^1];
            path.Remove(nextCell);
        }
    }

    private float pathfindTimer;
    [SerializeField] private float pathfindIntervalTimer;
    private void PathFindInterval()
    {
        pathfindTimer += Time.deltaTime;
        if(pathfindTimer >= pathfindIntervalTimer)
        {
            pathfindTimer = 0;
            StartPathFinding(target.transform.position);
        }
    }
}
