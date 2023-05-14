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

        private int HighScore3Hearts { get; set; }
        private int HighScoreRegular { get; set; }
        private int HighScoreEndless { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            HighScore3Hearts = PlayerPrefs.GetInt("HighScore_3Hearts", 0);
            HighScoreRegular = PlayerPrefs.GetInt("HighScore_Regular", 0);
            HighScoreEndless = PlayerPrefs.GetInt("HighScore_Endless", 0);
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

            UpdateScoreText();
            UpdateLinesClearedText();
            UpdateLevelText();
        }

        private int GetHighScore()
        {
            return GameManager.Instance.CurrentMode switch
            {
                "3Hearts" => HighScore3Hearts,
                "Regular" => HighScoreRegular,
                "Endless" => HighScoreEndless,
                _ => 0
            };
        }

        private void SetHighScore(int score)
        {
            switch (GameManager.Instance.CurrentMode)
            {
                case "3Hearts":
                    HighScore3Hearts = score;
                    PlayerPrefs.SetInt("HighScore_3Hearts", HighScore3Hearts);
                    break;
                case "Regular":
                    HighScoreRegular = score;
                    PlayerPrefs.SetInt("HighScore_Regular", HighScoreRegular);
                    break;
                case "Endless":
                    HighScoreEndless = score;
                    PlayerPrefs.SetInt("HighScore_Endless", HighScoreEndless);
                    break;
            }
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
                string highScoreLabel = "";

                switch (GameManager.Instance.CurrentMode)
                {
                    case "3Hearts":
                        highScoreLabel = $"High Score: {HighScore3Hearts}";
                        break;
                    case "Regular":
                        highScoreLabel = $"High Score: {HighScoreRegular}";
                        break;
                    case "Endless":
                        highScoreLabel = $"High Score: {HighScoreEndless}";
                        break;
                }

                highScoreText.text = highScoreLabel;
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