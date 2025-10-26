using System.Collections;
using System.Collections.Generic;
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

        private List<Unit.Enemy> _currentEnemies = new List<Unit.Enemy>();
        public List<Unit.Enemy> CurrentEnemies
        {
            get => _currentEnemies;
            private set => _currentEnemies = value;
        }

        private int _currentLevelEnemyCount;
        private int _currentLevelDiedEnemy;
        private int _currentLevelReachedEnemy;
        
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
            MessageBroker.Default.Receive<EnemyDiedMessage>().Subscribe(OnEnemyDied).AddTo(this);
            MessageBroker.Default.Receive<EnemyReachedToDefenderBaseMessage>().Subscribe(OnEnemyReachedToBase).AddTo(this);
        }

        private void OnEnemyReachedToBase(EnemyReachedToDefenderBaseMessage enemyReachedToDefenderBaseMessage)
        {
            _currentLevelReachedEnemy++;
        }

        private void OnEnemyDied(EnemyDiedMessage enemyDiedMessage)
        {
            if (CurrentEnemies.Contains(enemyDiedMessage.Diedenemy))
            {
                CurrentEnemies.Remove(enemyDiedMessage.Diedenemy);
                Destroy(enemyDiedMessage.Diedenemy.gameObject);
                _currentLevelDiedEnemy++;
                if ((_currentLevelDiedEnemy + _currentLevelReachedEnemy) == _currentLevelEnemyCount)
                {
                    _currentLevelDiedEnemy = 0;
                    _currentLevelReachedEnemy = 0;
                    MessageBroker.Default.Publish(new LevelCompletedMessage());
                }
            }
        }

        private void OnNewLevelReceived(NewLevelMessage msg)
        {
            foreach (var currentEnemy in CurrentEnemies)
            {
                Destroy(currentEnemy.gameObject);
            }
            CurrentEnemies.Clear();
            StartCoroutine(SpawnLevelEnemies(msg.LevelData));
        }

        private IEnumerator SpawnLevelEnemies(LevelDataSO currentLevelData)
        {
            CurrentEnemies = new List<Unit.Enemy>();
            var enemyList = new List<EnemyDataSo>(currentLevelData.EnemyData);
            enemyList.Shuffle();
            _currentLevelEnemyCount = enemyList.Count;
            foreach (var enemyDataSo in enemyList)
            {
                var availableSpawnCells = _gridManager.GetAvailableEnemySpawnCells();
                if (availableSpawnCells.Count == 0)
                {
                    Debug.LogWarning("No available spawn cells found.");
                    yield break;
                }

                var spawnCell = availableSpawnCells[Random.Range(0, availableSpawnCells.Count)];
                var enemy = _container.InstantiatePrefabForComponent<Unit.Enemy>(
                    enemyDataSo.UnitPrefab,
                    spawnCell.transform.position,
                    spawnCell.transform.rotation,
                    enemySpawnParent
                );
                enemy.Init(spawnCell);
                spawnCell.IsOccupied = true;
                spawnCell.UnitBase = enemy;
                CurrentEnemies.Add(enemy);
                yield return new WaitForSeconds(currentLevelData.EnemySpawnRateInSeconds);
            }
        }



    }
}
