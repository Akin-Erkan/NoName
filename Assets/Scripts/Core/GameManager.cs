using System.Collections;
using UnicoStudio.ScriptableObjects;
using UniRx;
using UnityEngine;

namespace UnicoStudio
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private LevelDataSO[] levelData;
        private int _currentLevel = 0;

        private LevelDataSO _currentLevelData;

        private float _breakTimeBetweenLevels = 3f; //for UI sound etc.
        
        private void Start()
        {
            StartFirstLevel();
        }

        private void StartFirstLevel()
        {
            _currentLevelData = levelData[_currentLevel];
            MessageBroker.Default.Publish(new NewLevelMessage(_currentLevelData));
            MessageBroker.Default.Receive<LevelCompletedMessage>().Subscribe(OnLevelCompleted).AddTo(this);
        }

        private void OnLevelCompleted(LevelCompletedMessage msg)
        {
            StartCoroutine(HandleLevelComplete());


        }

        private IEnumerator HandleLevelComplete()
        {
            _currentLevel++;
            if (levelData.Length > _currentLevel)
            {
                _currentLevelData = levelData[_currentLevel];
                print("You win the Round!");
                yield return new WaitForSeconds(_breakTimeBetweenLevels);
                MessageBroker.Default.Publish(new NewLevelMessage(_currentLevelData));
                print("New Level just started!");
            }
        }
        
        
    }
}

