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
        
        GridCell[,] _gridArray;

        private void Start()
        {
            GenerateGrid();
            
        }
        
        [ContextMenu("Generate Grid")]
        private void GenerateGrid()
        {
            _gridArray = new GridCell[rowSize, columnSize];
            for (int row = 0; row < rowSize; row++)
            {
                for (int column = 0; column < columnSize; column++)
                {
                    var gridCell = Instantiate(gridCellPrefab, gridCellParent);
                    var posX = column * gridCellSize;
                    var posY = row * -gridCellSize;
                    gridCell.transform.position = new Vector2(posX, posY);
                    gridCell.WorldPosition = new Vector2(posX, posY);
                    gridCell.GridPosition = new Vector2Int(row, column);
                    _gridArray[row, column] = gridCell;
                }
            }
            float gridWidth = columnSize * gridCellSize;
            float gridHeight = rowSize * gridCellSize;
            
            gridCellParent.transform.position = new Vector2(-gridWidth / 2 + gridCellSize / 2, gridHeight / 2 - gridCellSize / 2);
        }
        
        
        
    }
}
