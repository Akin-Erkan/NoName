using System;
using UniRx;
using UnicoStudio.ScriptableObjects;
using Zenject;
using UnityEngine;

namespace UnicoStudio.Unit
{
    public class Defender : UnitBase
    {
        private UnitTargetingSystem _unitTargetingSystem;
        private DefenceDataSo _defenderData;
        private bool _isAttackOnCooldown;
        [SerializeField]
        private Animator animator;

        private bool _canAttack = true;

        private bool _gameOver;

        [Inject]
        private void Construct(UnitTargetingSystem unitTargetingSystem)
        {
            _unitTargetingSystem = unitTargetingSystem;
        }

        protected override void Start()
        {
            base.Start();

            MessageBroker.Default.Receive<GameOverMessage>().Subscribe(OnGameOver).AddTo(this);
            MessageBroker.Default.Receive<LevelCompletedMessage>().Subscribe(OnLevelCompletedMessage).AddTo(this);
            _defenderData = UnitDataSo as DefenceDataSo;

            Observable.Interval(System.TimeSpan.FromSeconds(0.1f))
                .Subscribe(_ => TryAttack())
                .AddTo(this);
        }

        private void OnGameOver(GameOverMessage gameOverMessage)
        {
            _gameOver = true;
        }

        private void OnLevelCompletedMessage(LevelCompletedMessage levelCompletedMessage)
        {
            _canAttack = false;
        }

        private void TryAttack()
        {
            if(_gameOver)
                return;
            if(!_canAttack)
                return;
            if (_isAttackOnCooldown) 
                return;

            var nearestEnemy = _unitTargetingSystem.GetNearestEnemy(this);
            if (nearestEnemy == null) 
                return;

            AttackToEnemy(nearestEnemy);
            StartAttackCooldown();
        }

        private void AttackToEnemy(Enemy enemy)
        {
            enemy.TakeDamage(_defenderData.Damage);
            animator.SetTrigger("2_Attack");
            Debug.Log($"Attacked {enemy.UnitDataSo.DisplayName} for {_defenderData.Damage} damage");
        }

        private void StartAttackCooldown()
        {
            _isAttackOnCooldown = true;

            Observable.Timer(System.TimeSpan.FromSeconds(_defenderData.AttackInterval))
                .Subscribe(_ => _isAttackOnCooldown = false)
                .AddTo(this);
        }

        private void OnDestroy()
        {
            CurrentGridCell.IsOccupied = false;
        }
    }
}