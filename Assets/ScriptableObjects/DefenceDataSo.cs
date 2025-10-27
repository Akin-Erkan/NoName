using UnityEngine;

namespace UnicoStudio.ScriptableObjects
{
    public enum AttackDirection
    {
        Forward,
        All
    }
    
    
    [CreateAssetMenu(fileName = "DefenceItemData", menuName = "Game Data/Defence Item")]
    public class DefenceDataSo : UnitDataSO
    {
        [Header("Defence Specific")] 
        [SerializeField]
        private int damage;
        [SerializeField]
        private int range;
        [SerializeField]
        private float attackInterval;
        [SerializeField]
        private AttackDirection direction;

        public int Damage
        {
            get => damage;
            set => damage = value;
        }

        public int Range
        {
            get => range;
            set => range = value;
        }

        public float AttackInterval
        {
            get => attackInterval;
            set => attackInterval = value;
        }

        public AttackDirection Direction
        {
            get => direction;
            set => direction = value;
        }
    }


}