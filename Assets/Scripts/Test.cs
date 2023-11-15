using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridCell
{
    public class Test : MonoBehaviour
    {
        public static Test Instance;

        [SerializeField] private GameObject start;
        public GameObject target;
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSize;
        [SerializeField] private Vector3 origin;
        private Cell startCell = null;
        [HideInInspector]public Cell endCell = null;
        private AStar_Pathfinding aStar;
        private List<Cell> path;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            if(Instance != this)
            {
                Destroy(gameObject);
            }
            path = new List<Cell>();
            aStar = new AStar_Pathfinding(width, height, cellSize, origin);
        }

        public void Update()
        {
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    startCell = aStar.grid.GetCellFromWorldPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //    if(startCell != null)start.transform.position = startCell.GetCenterPos();
            //    Debug.Log("start");
            //}
            //if (Input.GetKeyDown(KeyCode.E))
            //{
            //    endCell = aStar.grid.GetCellFromWorldPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //    if(endCell != null)target.transform.position = endCell.GetCenterPos();
            //    Debug.Log("end");
            //}
            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    Cell cell = aStar.grid.GetCellFromWorldPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //    if(cell != null) cell.ChangeWalkable();
            //}    
            //if(Input.GetKeyDown(KeyCode.KeypadEnter))
            //{
            //    start.GetComponent<Enemy_Base>().ExitCurrentState();
            //}
            DebugPath();
            aStar.AStarUpdate();
        }

        private void DebugPath()
        {
            if(path.Count > 1)
            {
                startCell.DebugLineToNextCell(path[^1]);
                for (int i = 0; i < path.Count-1; i++)
                {
                    path[i].DebugLineToNextCell(path[i + 1]);
                }
            }
        }

        public void SetPath(List<Cell> path, Cell start)
        {
            this.path.Clear();
            startCell = start;
            foreach (Cell cell in path)
            {
                this.path.Add(cell);
            }
        }
        public AStar_Pathfinding GetAStar()
        {
            return aStar;
        }
    }
}
