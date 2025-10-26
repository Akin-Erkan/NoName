using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnicoStudio.Extensions;
using UnicoStudio.GridSystem;
using UnicoStudio.ScriptableObjects;
using UniRx;
using UnityEngine;
using Zenject;

namespace UnicoStudio.Enemy
{
    public class EnemySpawnController : MonoBehaviour
    {

        [SerializeField] 
        private Transform enemySpawnParent;
        
        private GridManager _gridManager;
        private DiContainer _container;

        [Inject]
        private void Construct(GridManager gridManager, DiContainer container)
        {
            _gridManager = gridManager;
            _container = container;
        }
        
        void Start()
        {
            MessageBroker.Default.Receive<NewLevelMessage>().Subscribe(OnNewLevelReceived).AddTo(this);
        }

        private void OnNewLevelReceived(NewLevelMessage msg)
        {
            StartCoroutine(SpawnLevelEnemies(msg.LevelData));
        }

        private IEnumerator SpawnLevelEnemies(LevelDataSO currentLevelData)
        {
            var enemyList = new List<EnemyDataSo>(currentLevelData.EnemyData);
            enemyList.Shuffle();
            foreach (var enemyDataSo in enemyList)
            {
                var availableSpawnCells = _gridManager.GetAvailableEnemySpawnCells();
                if (availableSpawnCells.Count == 0)
                {
                    Debug.LogWarning("No available spawn cells found.");
                    yield break;
                }

                var spawnCell = availableSpawnCells[Random.Range(0, availableSpawnCells.Count)];
                spawnCell.IsOccupied = true;

                var enemy = _container.InstantiatePrefabForComponent<Unit.Enemy>(
                    enemyDataSo.UnitPrefab,
                    spawnCell.transform.position,
                    spawnCell.transform.rotation,
                    enemySpawnParent
                );

                enemy.Init(spawnCell);
                yield return new WaitForSeconds(currentLevelData.EnemySpawnRateInSeconds);
            }
        }



    }
}
