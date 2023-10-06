using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridCell
{
    public class Test : MonoBehaviour
    {
        private Cell startCell = null;
        private Cell endCell = null;
        private AStar_Pathfinding aStar;
        private List<Cell> path;

        void Start()
        {
            path = new List<Cell>();
            aStar = new AStar_Pathfinding(20, 20, 2f, new Vector3(-5, -4, 0));
        }

        public void Update()
        {
            if (Input.GetMouseButton(0) && Input.GetKeyDown(KeyCode.S))
            {
                int x, y;
                aStar.grid.GetCellIndex(Camera.main.ScreenToWorldPoint(Input.mousePosition), out x, out y);
                if (x != -1 && y != -1)
                {
                    startCell = aStar.grid.GetCellFromIndex(x, y);
                    Debug.Log("start");
                }
            }
            if (Input.GetMouseButton(0) && Input.GetKeyDown(KeyCode.E))
            {
                int x, y;
                aStar.grid.GetCellIndex(Camera.main.ScreenToWorldPoint(Input.mousePosition), out x, out y);
                if (x != -1 && y != -1)
                {
                    endCell = aStar.grid.GetCellFromIndex(x, y); 
                    Debug.Log("end");

                }
            }
            if (Input.GetMouseButton(0) && Input.GetKeyDown(KeyCode.W))
            {
                int x, y;
                aStar.grid.GetCellIndex(Camera.main.ScreenToWorldPoint(Input.mousePosition), out x, out y);
                if (x != -1 && y != -1)
                {
                    Cell cell = aStar.grid.GetCellFromIndex(x, y);
                    cell.ChangeWalkable();

                }
            }    
            if(Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                path = aStar.ReturnPath(startCell, endCell);
                if(path.Count == 0)
                {
                    Debug.Log("NO PATH AVAILABLE!");
                    return;
                }
                DebugPath();
                Debug.Log("Path");
            }
            aStar.AStarUpdate();
        }

        private void DebugPath()
        {
            startCell.DebugLineToNextCell(path[^1]);
            for (int i = 0; i < path.Count-1; i++)
            {
                path[i].DebugLineToNextCell(path[i + 1]);
            }
        }
    }
}
