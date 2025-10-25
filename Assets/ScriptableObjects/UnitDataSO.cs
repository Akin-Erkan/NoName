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

        public Sprite Icon => icon;

        public string DisplayName => displayName;

        public string ID => id;
    }
    
}