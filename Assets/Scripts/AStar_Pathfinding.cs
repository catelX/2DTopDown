using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridCell
{
    public class AStar_Pathfinding
    {
        private List<Cell> openedList;
        private List<Cell> closedList;

        public Grid grid;
        private Cell currentCell;

        public AStar_Pathfinding(int width, int height, float cellSize, Vector3 origin)
        {
            grid = new Grid(width, height, cellSize, origin);
        }

        public void AStarUpdate()
        {
            grid.DebugCellWalkable();
        }

        public List<Cell> ReturnPath(Cell startCell, Cell endCell)
        {
            openedList = new List<Cell>();
            closedList = new List<Cell>();
            currentCell = startCell;
            openedList.Add(currentCell);
            List<Cell> path = new List<Cell>();
            List<Cell> neighbourList = new List<Cell>();

            while (currentCell != endCell)
            {
                neighbourList = grid.GetCellNeighbours(currentCell);
                foreach (Cell cell in neighbourList)
                {
                    if (closedList.Contains(cell) || openedList.Contains(cell) || !cell.isWalkable) continue;

                    cell.CalculateCosts(startCell, endCell);
                    cell.SetParent(currentCell);
                    openedList.Add(cell);
                }
                closedList.Add(currentCell);
                openedList.Remove(currentCell);
                if (openedList.Count == 0)
                {
                    path.Clear();
                    break;
                }
                currentCell = GetLowestFCostCell();
            }
            if(currentCell == endCell)
            {

                while(currentCell != startCell)
                {
                    path.Add(currentCell);
                    currentCell = currentCell.GetParent();
                }
            }

            return path;
        }

        public Cell GetLowestFCostCell()
        {
            float cost = 999;
            int index = 0;
            for (int i = 0; i < openedList.Count; i++)
            {
                float fCost = openedList[i].GetFCost();
                if(fCost < cost)
                {
                    index = i;
                    cost = fCost;
                }
            }
            return openedList[index];
        }
    }
}