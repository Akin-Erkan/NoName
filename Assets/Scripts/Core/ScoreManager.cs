using UnicoStudio.ScriptableObjects;
using UniRx;
using UnityEngine;

namespace UnicoStudio
{
    public class ScoreManager : MonoBehaviour
    {
        private int _currentScore = 0;
        private int _highScore = 0;

        private const string HighScoreKey = "HighScore";

        public int CurrentScore => _currentScore;
        public int HighScore => _highScore;

        private void Start()
        {
            _highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
            MessageBroker.Default.Receive<EnemyDiedMessage>().Subscribe(OnEnemyDied).AddTo(this);
            MessageBroker.Default.Receive<GameOverMessage>().Subscribe(OnGameOver).AddTo(this);
        }

        private void OnEnemyDied(EnemyDiedMessage enemyDiedMessage)
        {
            var enemyData = enemyDiedMessage.Diedenemy.UnitDataSo as EnemyDataSo;
            _currentScore += enemyData.ScoreValue;
        }

        private void OnGameOver(GameOverMessage gameOverMessage)
        {
            MessageBroker.Default.Publish(new GameOverScoreMessage(_currentScore));
            if (_currentScore > _highScore)
            {
                _highScore = _currentScore;
                PlayerPrefs.SetInt(HighScoreKey, _highScore);
                PlayerPrefs.Save();
                MessageBroker.Default.Publish(new NewHighScoreMessage(_highScore));
            }
        }
    }


}