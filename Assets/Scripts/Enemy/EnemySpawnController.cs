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

        private int _currentLevel = 0;
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
            HandleEnemyDieOrDefenderBaseReach(enemyReachedToDefenderBaseMessage.ReachedEnemy,enemyReachedToDefenderBaseMessage.DestroyAfterSeconds);
        }

        private void OnEnemyDied(EnemyDiedMessage enemyDiedMessage)
        {
            _currentLevelDiedEnemy++;
            HandleEnemyDieOrDefenderBaseReach(enemyDiedMessage.Diedenemy,enemyDiedMessage.DestroyAfterSeconds);
        }

        private void HandleEnemyDieOrDefenderBaseReach(Unit.Enemy enemy, float destroyAfterSeconds)
        {
            if (CurrentEnemies.Contains(enemy))
            {
                CurrentEnemies.Remove(enemy);
                Destroy(enemy.gameObject, destroyAfterSeconds);
                if ((_currentLevelDiedEnemy + _currentLevelReachedEnemy) >= _currentLevelEnemyCount)
                {
                    var newLevelCompeteInfo = new LevelCompleteInfo(_currentLevel,
                        _currentLevelDiedEnemy,
                        _currentLevelReachedEnemy);
                    MessageBroker.Default.Publish(new LevelCompletedMessage(newLevelCompeteInfo));
                }
            }
        }

        private void OnNewLevelReceived(NewLevelMessage msg)
        {
            _currentLevel = msg.CurrentLevel;
            _currentLevelDiedEnemy = 0;
            _currentLevelReachedEnemy = 0;
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
