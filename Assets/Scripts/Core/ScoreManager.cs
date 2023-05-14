using TMPro;
using UnityEngine;

namespace Tetris.Core
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI linesClearedText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI highScoreText;
        public static ScoreManager Instance { get; private set; }

        private int Score { get; set; }
        private int LinesCleared { get; set; }
        private int Level { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (Instance != this)
            {
                Instance = this;
                Destroy(Instance);
                UpdateHighScoreText();
            }
        }

        private void Start()
        {
            UpdateHighScoreText();
        }

        public void AddScore(int linesCleared)
        {
            LinesCleared += linesCleared;
            Level = LinesCleared / 10;

            int points = linesCleared switch
            {
                1 => 40,
                2 => 100,
                3 => 300,
                4 => 1200,
                _ => 0
            };

            Score += points;

            if (Score > GetHighScore())
            {
                SetHighScore(Score);
                UpdateHighScoreText();
            }
            
            Debug.Log($"AddScore: {LinesCleared}, {Level}, {Score}, {GetUpdatedStepDelay()}");

            UpdateScoreText();
            UpdateLinesClearedText();
            UpdateLevelText();
        }

        private static int GetHighScore()
        {
            string key = $"{GameManager.Instance.CurrentMode}_{DifficultyManager.Instance.DifficultyLevel}";
            return PlayerPrefs.GetInt($"HighScore_{key}", 0);
        }

        private static void SetHighScore(int score)
        {
            string key = $"{GameManager.Instance.CurrentMode}_{DifficultyManager.Instance.DifficultyLevel}";
            PlayerPrefs.SetInt($"HighScore_{key}", score);
            PlayerPrefs.Save();
        }

        private void UpdateScoreText()
        {
            if (scoreText)
            {
                scoreText.text = $"Taškai: {Score}";
            }
        }

        private void UpdateLevelText()
        {
            if (levelText)
            {
                levelText.text = $"Lygis: {Level}";
            }
        }

        private void UpdateLinesClearedText()
        {
            if (linesClearedText)
            {
                linesClearedText.text = $"Išvalytos linijos: {LinesCleared}";
            }
        }

        private void UpdateHighScoreText()
        {
            if (highScoreText)
            {
                int highScore = GetHighScore();
                highScoreText.text = $"High Score: {highScore}";
            }
        }

        public float GetUpdatedStepDelay()
        {
            return DifficultyManager.Instance.GetStepDelay(LinesCleared);
        }

        public void ResetScore()
        {
            Score = 0;
            LinesCleared = 0;
            Level = 0;

            UpdateScoreText();
            UpdateLinesClearedText();
            UpdateLevelText();
        }
    }
}