using UnicoStudio.GridSystem;
using UnicoStudio.ScriptableObjects;
using UnityEngine;

namespace UnicoStudio.Unit
{
    public abstract class UnitBase : MonoBehaviour
    {
        public int CurrentHP { get; protected set; }
        
        [SerializeField]
        private UnitDataSO unitDataSo;
        public UnitDataSO UnitDataSo
        {
            get => unitDataSo;
        }

        protected GridCell _currentGridCell;

        

    }
    
    
}
