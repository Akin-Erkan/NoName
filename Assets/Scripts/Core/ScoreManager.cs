using UnicoStudio.ScriptableObjects;
using UniRx;
using UnityEngine;

namespace UnicoStudio
{
    public class ScoreManager : MonoBehaviour
    {
        private int _currentScore = 0;

        public int CurrentScore
        {
            get => _currentScore;
            private set => _currentScore = value;
        }

        private void Start()
        {
            MessageBroker.Default.Receive<EnemyDiedMessage>().Subscribe(OnEnemyDied).AddTo(this);
            MessageBroker.Default.Receive<GameOverMessage>().Subscribe(OnGameOver).AddTo(this);
        }

        private void OnEnemyDied(EnemyDiedMessage enemyDiedMessage)
        {
            var enemyData = enemyDiedMessage.Diedenemy.UnitDataSo as EnemyDataSo;
            CurrentScore += enemyData.ScoreValue;
        }

        private void OnGameOver(GameOverMessage gameOverMessage)
        {
            MessageBroker.Default.Publish(new GameOverScoreMessage(CurrentScore));
        }
        
        
    }
}
