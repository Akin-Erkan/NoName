using UnityEngine;

namespace UnicoStudio.ScriptableObjects
{
    public enum AttackDirection
    {
        Forward,
        Backward,
        Left,
        Right,
        All
    }
    
    
    [CreateAssetMenu(fileName = "DefenceItemData", menuName = "Game Data/Defence Item")]
    public class DefenceItemDataSO : UnitData
    {
        [Header("Defence Specific")] 
        public int damage;
        public float range;    
        public float attackInterval;  
        public AttackDirection direction;
    }


}