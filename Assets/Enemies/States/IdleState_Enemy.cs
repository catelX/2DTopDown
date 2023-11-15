using System.Collections;
using System.Collections.Generic;
using GridCell;
using UnityEngine;

public class IdleState_Enemy : EnemyState_Base
{
    private Animator anim;
    private AStar_Pathfinding aStar;
    private GameObject target;
    private Cell nextCell;
    private Vector2 moveDir;
    [SerializeField] private GameObject parent;

    public override void EnterState(GameObject target)
    {
        anim.Play("Idle");
        timer = 0;
        this.target = target;
        SetNextCell();
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

    private float timer;
    public override void StateUpdate()
    {
        if (nextCell == null || 
            Vector2.Distance(parent.transform.position, nextCell.GetCenterPos()) < 0.2f)
        {
            moveDir = Vector2.zero;
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                timer = 0;
                SetNextCell();
            }
        }
        else
        {
            moveDir = nextCell.GetCenterPos() - parent.transform.position;
        }
    }

    private void SetNextCell()
    {
        List<Cell> neighbours = aStar.grid.GetCellNeighbours(aStar.grid.GetCellFromWorldPos(parent.transform.position));
        int ranIndex = Random.Range(0, neighbours.Count);
        nextCell = neighbours[ranIndex];
    }
}
