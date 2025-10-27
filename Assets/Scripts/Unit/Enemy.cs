using System;
using System.Collections;
using UnicoStudio.GridSystem;
using UnicoStudio.ScriptableObjects;
using UniRx;
using UnityEngine;
using Zenject;

namespace UnicoStudio.Unit
{
    public class Enemy : UnitBase
    {
        private GridManager _gridManager;
        
        private int _currentHealth;
        private bool _canTakeDamage = true;
        [SerializeField]
        private Animator animator;
        private bool _gameOver;

        [Inject]
        private void Construct(GridManager gridManager)
        {
            _gridManager = gridManager;
        }
        
        protected override void Start()
        {
            base.Start();
            MessageBroker.Default.Receive<LevelCompletedMessage>().Subscribe(OnLevelCompleted).AddTo(this);
            MessageBroker.Default.Receive<GameOverMessage>().Subscribe(OnGameOver).AddTo(this);
            
            var enemyData = UnitDataSo as EnemyDataSo;
            _currentHealth = enemyData.Health;
            StartCoroutine(MoveToDefenderBase());
        }

        public override void Init(GridCell gridCell)
        {
            base.Init(gridCell);
        }

        private void OnGameOver(GameOverMessage gameOverMessage)
        {
            _gameOver = true;
            StopAllCoroutines();
        }

        private void OnLevelCompleted(LevelCompletedMessage levelCompletedMessage)
        {
            StopAllCoroutines();
        }

        private IEnumerator MoveToDefenderBase()
        {
            var nextCell = _gridManager.GetNextAvailableGridCellForEnemy(CurrentGridCell);
            
            var enemyData = UnitDataSo as EnemyDataSo;
            var speed = enemyData.Speed;
            var moveDuration = 1f/speed;
            
                        
            Vector3 startPos = transform.position;
            Vector3 endPos = nextCell.transform.position;
            float elapsed = 0f;
            
            while (elapsed < moveDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / moveDuration);
                transform.position = Vector3.Lerp(startPos, endPos, t);
                yield return null;
            }
            transform.position = endPos;
            
            nextCell.IsOccupied = true;
            nextCell.UnitBase = this;
            CurrentGridCell.IsOccupied = false;
            CurrentGridCell.UnitBase = null;
            CurrentGridCell = nextCell;
            if (CurrentGridCell.GridPosition.y == _gridManager.GetRowSize() -1 )
            {
                EnemyReachedToDefenderBase(moveDuration);
                var lastMovementDistance = endPos - startPos;
                var lastMovePosition = transform.position + lastMovementDistance;
                startPos = transform.position;
                elapsed = 0;
                while (elapsed < moveDuration)
                {
                    elapsed += Time.deltaTime;
                    float t = Mathf.Clamp01(elapsed / moveDuration);
                    transform.position = Vector3.Lerp(startPos, lastMovePosition, t);
                    yield return null;
                }
                yield break;
            }
            
            StartCoroutine(MoveToDefenderBase());
            
        }

        public void TakeDamage(int damage)
        {
            if (_gameOver)
                return;
            if(!_canTakeDamage)
                return;
            if (_currentHealth > 0)
            {
                _currentHealth -= damage;
                animator.SetTrigger("3_Damaged");
            }

            if (_currentHealth <= 0)
            {
                _canTakeDamage = false;
                animator.SetBool("isDeath", true);
                animator.SetTrigger("4_Death");
                MessageBroker.Default.Publish(new EnemyDiedMessage(this,1f));
                StopAllCoroutines();
            }
            
        }

        public void EnemyReachedToDefenderBase(float destroyAfterSeconds)
        {
            _canTakeDamage = false;
            MessageBroker.Default.Publish(new EnemyReachedToDefenderBaseMessage(this,destroyAfterSeconds));
            print("EnemyReachedToDefenderBaseMessage");
        }

        private void OnDestroy()
        {
            CurrentGridCell.IsOccupied = false;
        }
    }

}
