using UnicoStudio.GridSystem;
using UniRx;
using UnityEngine;
using Zenject;

namespace UnicoStudio.Core
{
    public class UnitPlacementController : MonoBehaviour
    {
        private GridManager _gridManager;

        private Camera _currentCamera;

        [SerializeField] private LayerMask gridLayer;

        [Inject]
        private void Construct(GridManager gridManager, Camera currentCamera)
        {
            _gridManager = gridManager;
            _currentCamera = currentCamera;
        }

        private void Start()
        {
            MessageBroker.Default.Receive<UnitDragMessage>().Subscribe(OnUnitDrag).AddTo(this);
        }

        private void OnUnitDrag(UnitDragMessage unitDragMessage)
        {
            var gridCell = CheckGridCellSelection();
            if (gridCell == null) 
                return;
            MessageBroker.Default.Publish(new UnitSpawnMessage(unitDragMessage.DraggedUnitInfo,gridCell));
            
        }


        public GridCell CheckGridCellSelection()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Ended:
                        Ray ray = _currentCamera.ScreenPointToRay(Input.touches[0].position);
                        Vector2 worldPoint = ray.origin;
                        RaycastHit2D hit = Physics2D.Raycast(worldPoint, ray.direction, Mathf.Infinity, gridLayer);
                        if (hit.collider != null)
                        {
                            Debug.Log("Hit grid cell");
                            if (hit.transform.TryGetComponent(out GridCell gridCell))
                            {
                                if (!gridCell.IsOccupied && _gridManager.IsPlacementAllowed(gridCell))
                                {
                                    print("Placement allowed");
                                    return gridCell;
                                }
                                else
                                {
                                    print("Not Placement allowed on this grid.");
                                }
                            }
                        }

                        break;
                }
            }

            return null;
        }
    }
}