using UnityEngine;

namespace UnicoStudio.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Game Data/Enemy")]
    public class EnemyDataSo : UnitDataSO
    {
        [Header("Enemy Specific")] 
        [SerializeField]
        private int health;
        [SerializeField]
        private float speed;


        [Header("Reward")]
        [SerializeField]
        private int scoreValue;


        public int Health
        {
            get => health;
            set => health = value;
        }

        public int ScoreValue
        {
            get => scoreValue;
            set => scoreValue = value;
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }
    }
    
}