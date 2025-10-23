using UnityEngine;

namespace UnicoStudio.ScriptableObjects
{
    public abstract class UnitData : ScriptableObject
    {
        [Header("General Info")]
        public string id;
        public string displayName;
        public Sprite icon;
    }
    
}