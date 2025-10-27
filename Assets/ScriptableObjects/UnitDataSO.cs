using UnicoStudio.Unit;
using UnityEngine;

namespace UnicoStudio.ScriptableObjects
{
    public abstract class UnitDataSO : ScriptableObject
    {
        [Header("General Info")]
        [SerializeField]
        private string id;
        [SerializeField]
        private string displayName;
        [SerializeField]
        private Sprite icon;
        [SerializeField]
        private UnitBase unitPrefab;

        public Sprite Icon => icon;

        public string DisplayName => displayName;

        public string ID => id;
        
        public UnitBase UnitPrefab => unitPrefab;
    }
    
}