using System.Collections;
using UnicoStudio.ScriptableObjects;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            MessageBroker.Default.Receive<LevelCompletedMessage>().Subscribe(OnLevelCompleted).AddTo(this);
            MessageBroker.Default.Receive<RestartRequestMessage>().Subscribe(OnRestartRequested).AddTo(this);
            StartFirstLevel();
        }

        private void StartFirstLevel()
        {
            _currentLevelData = levelData[_currentLevel];
            MessageBroker.Default.Publish(new NewLevelMessage(_currentLevel,_currentLevelData));
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
                MessageBroker.Default.Publish(new NewLevelMessage(_currentLevel,_currentLevelData));
                print("New Level just started!");
            }
            else
            {
                //TODO: Gameover
                MessageBroker.Default.Publish(new GameOverMessage());
                
            }
        }

        private void OnRestartRequested(RestartRequestMessage msg)
        {
           // restart the current scene
           var currentScene = SceneManager.GetActiveScene();
           SceneManager.LoadScene(currentScene.name);
        }


    }
}

