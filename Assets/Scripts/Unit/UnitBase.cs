using UnicoStudio.GridSystem;
using UnicoStudio.ScriptableObjects;
using UnityEngine;

namespace UnicoStudio.Unit
{
    public abstract class UnitBase : MonoBehaviour
    {
        
        [SerializeField]
        private UnitDataSO unitDataSo;
        public UnitDataSO UnitDataSo
        {
            get => unitDataSo;
        }
        
        protected GridCell _currentGridCell;
        
        private SpriteRenderer _spriteRenderer;
        
        public SpriteRenderer SpriteRenderer
        {
            get => _spriteRenderer;
            set => _spriteRenderer = value;
        }

        public GridCell CurrentGridCell
        {
            get => _currentGridCell;
            set => _currentGridCell = value;
        }


        protected virtual void Start()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public virtual void Init(GridCell gridCell)
        {
            if(SpriteRenderer == null)
                SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer.sprite = unitDataSo.Icon;
            CurrentGridCell = gridCell;
        }
        
        
        
        
    }
    
    
}
