using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UnicoStudio.UI
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField]
        private Transform gameInfoPanel;
        [SerializeField]
        private TextMeshProUGUI newLevelInfoText;

        [SerializeField] 
        private Transform gameOverPanel;
        [SerializeField]
        private TextMeshProUGUI gameOverText;
        [SerializeField]
        private TextMeshProUGUI highScoreText;
        
        [SerializeField]
        private Transform highScorePanel;
        
        [SerializeField]
        private Button restartButton;
        
        void Start()
        {
            MessageBroker.Default.Receive<LevelCompletedMessage>().Subscribe(OnLevelComplete).AddTo(this);
            MessageBroker.Default.Receive<GameOverScoreMessage>().Subscribe(OnGameOver).AddTo(this);
            MessageBroker.Default.Receive<NewHighScoreMessage>().Subscribe(OnNewHighScore).AddTo(this);
            restartButton.onClick.AddListener(OnRestartClicked);
        }


        private void OnLevelComplete(LevelCompletedMessage levelCompletedMessage)
        {
            gameInfoPanel.gameObject.SetActive(true);
            newLevelInfoText.text =
                $"You Just Completed Level:{levelCompletedMessage.LevelCompleteInfo.Level + 1} \nNew Wave Incoming!!!";
            Observable.Interval(System.TimeSpan.FromSeconds(2f))
                .Subscribe(_ =>
                {
                    newLevelInfoText.text = "";
                    gameInfoPanel.gameObject.SetActive(false);
                })
                .AddTo(this);

        }

        private void OnGameOver(GameOverScoreMessage gameOverScoreMessage)
        {
            gameOverPanel.gameObject.SetActive(true);
            gameOverText.text = "Game Over! \n Your score is:  " + gameOverScoreMessage.Score;
            var highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = $"High Score: {highScore}";

        }

        private void OnNewHighScore(NewHighScoreMessage newHighScoreMessage)
        {
            highScorePanel.gameObject.SetActive(true);
            highScoreText.text = $"High Score: {newHighScoreMessage.NewHighScore}";
        }

        private void OnRestartClicked()
        {
            MessageBroker.Default.Publish(new RestartRequestMessage());
        }


    }
}