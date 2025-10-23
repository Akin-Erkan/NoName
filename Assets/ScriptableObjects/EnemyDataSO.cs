using UnityEngine;

namespace UnicoStudio.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Game Data/Enemy")]
    public class EnemyDataSO : UnitData
    {
        [Header("Enemy Specific")]
        public int health;
        public float speed;


        [Header("Reward")] 
        public int scoreValue;
        public int goldReward;
        
    }
    
}