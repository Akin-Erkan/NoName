using System.Collections.Generic;
using UnityEngine;

namespace UnicoStudio.GridSystem
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField]
        private int rowSize;
        [SerializeField]
        private int columnSize;
        [SerializeField] 
        private float gridCellSize = 1f;
        [SerializeField]
        private GridCell gridCellPrefab;
        [SerializeField]
        private Transform gridCellParent;
        [SerializeField]
        private Vector2 gridParentScreenOffset;
        
        GridCell[,] _gridArray;
        
        private void Start()
        {
            GenerateGrid();
        }
        
        [ContextMenu("Generate Grid")]
        private void GenerateGrid()
        {
            _gridArray = new GridCell[columnSize, rowSize];
            for (int column = 0; column < columnSize; column++)
            {
                for (int row = 0; row < rowSize; row++)
                {
                    var gridCell = Instantiate(gridCellPrefab, gridCellParent);
                    var posX = column * gridCellSize;
                    var posY = row * -gridCellSize;
                    gridCell.transform.position = new Vector2(posX, posY);
                    gridCell.WorldPosition = new Vector2(posX, posY);
                    gridCell.GridPosition = new Vector2Int(column, row);
                    _gridArray[column,row] = gridCell;
                }
            }
            float gridWidth = columnSize * gridCellSize;
            float gridHeight = rowSize * gridCellSize;
            
            gridCellParent.transform.position = new Vector2(-gridWidth / 2 + gridCellSize / 2 + gridParentScreenOffset.x
                , gridHeight / 2 - gridCellSize / 2 + gridParentScreenOffset.y);
        }
        
        
        
        public bool IsPlacementAllowed(GridCell gridCell) => gridCell.GridPosition.y >= rowSize / 2;

        public GridCell GetCell(int x, int y) => x >= 0 && x < columnSize && y >= 0 && y < rowSize ? _gridArray[x, y] : null;

        public List<GridCell> GetAvailableEnemySpawnCells()
        {
            List<GridCell> availableEnemySpawnCells = new List<GridCell>();

            for (int column = 0; column < columnSize; column++)
            {
                for (int row = 0; row < 1; row++)
                {
                    var gridCell = _gridArray[column, row];
                    if(!gridCell.IsOccupied)
                        availableEnemySpawnCells.Add(_gridArray[column, row]);
                }
            }
            return availableEnemySpawnCells;
        }

        public GridCell GetNextAvailableGridCellForEnemy(GridCell gridCell)
        {
            GridCell nextAvailableGridCell = null;
            if (gridCell.GridPosition.y < rowSize -1)
            {
                var nextGridCell = _gridArray[gridCell.GridPosition.x, gridCell.GridPosition.y +1];
                if(!nextGridCell.IsOccupied)
                    nextAvailableGridCell = nextGridCell;
            }
            return nextAvailableGridCell;
        }
        
        
    }
    

}
