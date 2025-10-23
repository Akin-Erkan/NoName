using UnityEngine;

namespace UnicoStudio.GridSystem
{
    public class GridCell : MonoBehaviour
    {
        private Vector2 _worldPosition;

        public Vector2 WorldPosition
        {
            get => _worldPosition;
            set => _worldPosition = value;
        }

        private Vector2Int _gridPosition;

        public Vector2Int GridPosition
        {
            get => _gridPosition;
            set => _gridPosition = value;
        }

        private bool _isOccupied;

        public bool IsOccupied
        {
            get => _isOccupied;
            set => _isOccupied = value;
        }
    }
}