using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridCell
{
    [System.Serializable]
    public class Grid
    {
        private int width, height;
        private float cellSize;
        private Cell[,] gridArray;
        private Vector3 origin;

        public Grid(int width, int height, float cellSize, Vector3 origin)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.origin = origin;

            gridArray = new Cell[width, height];

            for (int i = 0; i < width; i++)
            {
                float x = i * cellSize;
                for (int j = 0; j < height; j++)
                {
                    float y = j * cellSize;
                    gridArray[i, j] = new Cell(i, j, new Vector3(x, y, 0) + origin, cellSize);
                }
            }
        }

        public Cell GetCellFromIndex(int x, int y)
        {
            return gridArray[x, y];
        }

        public Cell GetCellFromWorldPos(Vector3 worldPosition)
        {
            float xIndex = Mathf.Floor((worldPosition.x - origin.x) / cellSize);
            float yIndex = Mathf.Floor((worldPosition.y - origin.y) / cellSize);

            if (xIndex >= 0 && yIndex >= 0 && xIndex < width && yIndex < height)
            {
                return GetCellFromIndex((int)xIndex, (int)yIndex);
            
            }
            return null;
        }

        public void DebugCellWalkable()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    gridArray[i, j].WalkableVisual();
                }
            }
        }

        public List<Cell> GetCellNeighbours(Cell cell)
        {
            Vector2 index = cell.GetIndex();

            List<Cell> cellList = new List<Cell>();
            if (index.x > 0 && index.x < width)
            {
                cellList.Add(GetCellFromIndex((int)index.x - 1, (int)index.y));
                if(index.y < height-1)
                {
                    cellList.Add(GetCellFromIndex((int)index.x - 1, (int)index.y + 1));
                }
                if(index.y > 0)
                {
                    cellList.Add(GetCellFromIndex((int)index.x - 1, (int)index.y - 1));
                }
            }
            if (index.x < width - 1 && index.x >= 0)
            {
                cellList.Add(GetCellFromIndex((int)index.x + 1, (int)index.y));
                if (index.y < height - 1)
                {
                    cellList.Add(GetCellFromIndex((int)index.x + 1, (int)index.y + 1));
                }
                if (index.y > 0)
                {
                    cellList.Add(GetCellFromIndex((int)index.x + 1, (int)index.y - 1));
                }
            }
            if (index.y > 0 && index.y < height)
            {
                cellList.Add(GetCellFromIndex((int)index.x, (int)index.y - 1));
            }
            if (index.y < height - 1 && index.y >= 0)
            {
                cellList.Add(GetCellFromIndex((int)index.x, (int)index.y + 1));
            }
            foreach (Cell item in cellList)
            {
                if (!item.isWalkable) cellList.Remove(item);
            }
            return cellList;
        }

        public void DebugGrid()
        {
            for (int i = 0; i < width; i++)
            {
                float x = i * cellSize;
                for (int j = 0; j < height; j++)
                {
                    float y = j * cellSize;
                    Debug.DrawLine(new Vector3(x, y, 0) + origin, new Vector3(x + cellSize, y, 0) + origin, new Color(255, 255, 255));
                    Debug.DrawLine(new Vector3(x, y, 0) + origin, new Vector3(x, y + cellSize, 0) + origin, new Color(255, 255, 255));
                }
            }

            Debug.DrawLine(new Vector3(0, height * cellSize, 0) + origin, new Vector3(width * cellSize, height * cellSize, 0) + origin, new Color(255, 255, 255));
            Debug.DrawLine(new Vector3(width * cellSize, height * cellSize, 0) + origin, new Vector3(width * cellSize, 0, 0) + origin, new Color(255, 255, 255));
        }
    }
}
