using UnicoStudio.ScriptableObjects;
using UnicoStudio.Unit;
using UnityEngine;

namespace UnicoStudio.GridSystem
{
    public class GridCell : MonoBehaviour
    {
        private Vector2Int _gridPosition;
        private Vector2 _worldPosition;
        private bool _isOccupied;
        private UnitBase _unitBase;


        public Vector2Int GridPosition
        {
            get => _gridPosition;
            set => _gridPosition = value;
        }
        

        public Vector2 WorldPosition
        {
            get => _worldPosition;
            set => _worldPosition = value;
        }
        
        public bool IsOccupied
        {
            get => _isOccupied;
            set => _isOccupied = value;
        }

        public UnitBase UnitBase
        {
            get => _unitBase;
            set => _unitBase = value;
        }
    }
}