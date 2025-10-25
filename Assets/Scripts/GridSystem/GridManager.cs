using UniRx;
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

        [SerializeField] 
        private Camera currentCamera;
        [SerializeField]
        private LayerMask gridLayer;

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
            
            gridCellParent.transform.position = new Vector2(-gridWidth / 2 + gridCellSize / 2 + gridParentScreenOffset.x
                , gridHeight / 2 - gridCellSize / 2 + gridParentScreenOffset.y);
        }


        private void Update()
        {
            CheckGridCellSelection();
        }

        public void CheckGridCellSelection()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Ended:
                        Ray ray = currentCamera.ScreenPointToRay(Input.touches[0].position);
                        Vector2 worldPoint = ray.origin;
                        RaycastHit2D hit = Physics2D.Raycast(worldPoint, ray.direction, Mathf.Infinity, gridLayer);
                        if (hit.collider != null)
                        {
                            Debug.Log("Hit grid cell");
                            if (hit.transform.TryGetComponent(out GridCell gridCell))
                            {
                                if (IsPlacementAllowed(gridCell.GridPosition.x,gridCell.GridPosition.y) && !gridCell.IsOccupied)
                                {
                                    MessageBroker.Default.Publish(new GridSelectionMessage(gridCell));
                                }
                            }
                        }
                        break;
                }
            }
        }

        
        
        public bool IsPlacementAllowed(int x, int y) => y < rowSize / 2;

        public GridCell GetCell(int x, int y) => x >= 0 && x < columnSize && y >= 0 && y < rowSize ? _gridArray[x, y] : null;
        
    }
    

}
