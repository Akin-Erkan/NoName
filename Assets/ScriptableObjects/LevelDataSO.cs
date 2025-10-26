using System.Collections.Generic;
using UnityEngine;


namespace UnicoStudio.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Game Data/Level Data")]
    public class LevelDataSO : ScriptableObject
    {
        [Header("General Info")]
        [SerializeField]
        private string id;
        [SerializeField]
        private string displayName;
        
        [Header("Level Data")]
        [SerializeField]
        private List<DefenceDataSo> defenderData;
        [SerializeField] 
        private List<EnemyDataSo> enemyData;
        [SerializeField] 
        private float enemySpawnRateInSeconds;
        [SerializeField]
        private int baseHealth;

        public List<DefenceDataSo> DefenderData
        {
            get => defenderData;
            set => defenderData = value;
        }

        public List<EnemyDataSo> EnemyData
        {
            get => enemyData;
            set => enemyData = value;
        }

        public float EnemySpawnRateInSeconds
        {
            get => enemySpawnRateInSeconds;
            set => enemySpawnRateInSeconds = value;
        }

        public int BaseHealth
        {
            get => baseHealth;
            set => baseHealth = value;
        }
    }
}
