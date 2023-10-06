using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridCell
{
    [System.Serializable]
    public class Cell
    {
        public int indexX, indexY;
        public float gCost, hCost, fCost;
        public Vector3 origin;
        public float cellSize;
        private Cell parentCell;

        public bool isWalkable = true;

        public Cell(int x, int y, Vector3 origin, float cellSize)
        {
            this.indexX = x;
            this.indexY = y;
            this.origin = origin;
            this.cellSize = cellSize;
        }

        public void WalkableVisual()
        {
            if (!isWalkable)
            {
                Debug.DrawLine(origin, new Vector3(origin.x + cellSize, origin.y + cellSize), new Color(255, 0, 0));
                Debug.DrawLine(new Vector3(origin.x, origin.y + cellSize), new Vector3(origin.x + cellSize, origin.y), new Color(255, 0, 0));
            }
        }

        public Vector2 GetIndex()
        {
            return new Vector2(indexX, indexY);
        }

        public void ChangeWalkable()
        {
            isWalkable = !isWalkable;
        }

        public void CalculateCosts(Cell startCell, Cell endCell)
        {
            CalculateGandHCost(startCell, endCell);
            fCost = gCost + hCost;
        }

        private void CalculateGandHCost(Cell startCell, Cell endCell)
        {
            // GCost Calculate
            float distance = 0;
            Vector2 index = startCell.GetIndex();
            distance += Mathf.Abs(index.x - indexX);
            distance += Mathf.Abs(index.y - indexY);

            gCost = distance * 10;

            // HCost Calculate
            distance = 0;
            Vector2 otherIndex = endCell.GetIndex();
            distance += Mathf.Abs(otherIndex.x - indexX);
            distance += Mathf.Abs(otherIndex.y - indexY);

            hCost = distance * 10;
        }

        public float GetFCost()
        {
            return fCost;
        }

        public void SetParent(Cell parent)
        {
            parentCell = parent;
        }

        public Cell GetParent()
        {
            return parentCell;
        }

        public void DebugLineToNextCell(Cell cell)
        {
            Debug.DrawLine(new Vector3(origin.x + (cellSize / 2), origin.y + (cellSize / 2)), new Vector3(cell.origin.x + (cell.cellSize / 2), cell.origin.y + (cell.cellSize / 2)), Color.green, 100f);
        }

        public void DrawRect(Color color, float duration)
        {
            Debug.DrawLine(new Vector3(origin.x + 0.2f, origin.y + 0.2f), new Vector3(origin.x + cellSize - 0.2f, origin.y + 0.2f), color, duration);
            Debug.DrawLine(new Vector3(origin.x + cellSize - 0.2f, origin.y + 0.2f), new Vector3(origin.x + cellSize - 0.2f, origin.y + cellSize - 0.2f), color, duration);
            Debug.DrawLine(new Vector3(origin.x + cellSize - 0.2f, origin.y + cellSize - 0.2f), new Vector3(origin.x - 0.2f, origin.y + cellSize - 0.2f), color, duration);
            Debug.DrawLine(new Vector3(origin.x - 0.2f, origin.y + cellSize - 0.2f), new Vector3(origin.x + 0.2f, origin.y + 0.2f), color, duration);
        }
    }
}
