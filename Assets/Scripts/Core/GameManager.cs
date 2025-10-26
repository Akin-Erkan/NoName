using UnicoStudio.ScriptableObjects;
using UniRx;
using UnityEngine;

namespace UnicoStudio
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private LevelDataSO[] levelData;

        private LevelDataSO _currentLevelData;
        
        private void Start()
        {
            StartFirstLevel();
        }

        private void StartFirstLevel()
        {
            _currentLevelData = levelData[0];
            MessageBroker.Default.Publish(new NewLevelMessage(_currentLevelData));
        }
        
    }
}

