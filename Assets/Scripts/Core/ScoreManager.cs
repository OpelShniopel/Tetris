using TMPro;
using UnityEngine;

namespace Tetris.Core
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI linesClearedText;
        [SerializeField] private TextMeshProUGUI levelText;
        public static ScoreManager Instance { get; private set; }

        private int Score { get; set; }
        private int LinesCleared { get; set; }
        private int Level { get; set; }

        private Difficulty DifficultyLevel { get; set; }

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

            UpdateScoreText();
            UpdateLinesClearedText();
            UpdateLevelText();
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

        public float GetUpdatedStepDelay()
        {
            float initialSpeed = 1f;
            float speedIncrease = 0.05f * LinesCleared;
            float minStepDelay = 0.05f;

            // Adjust initial speed and speed increase rate based on the difficulty level
            switch (DifficultyLevel)
            {
                case Difficulty.Easy:
                    initialSpeed = 1f;
                    speedIncrease = 0.05f * LinesCleared;
                    break;
                case Difficulty.Medium:
                    initialSpeed = 0.75f;
                    speedIncrease = 0.1f * LinesCleared;
                    break;
                case Difficulty.Hard:
                    initialSpeed = 0.5f;
                    speedIncrease = 0.15f * LinesCleared;
                    break;
            }

            return Mathf.Max(initialSpeed - speedIncrease, minStepDelay);
        }

        public void SetDifficultyEasy()
        {
            SetDifficulty(Difficulty.Easy);
        }

        public void SetDifficultyMedium()
        {
            SetDifficulty(Difficulty.Medium);
        }

        public void SetDifficultyHard()
        {
            SetDifficulty(Difficulty.Hard);
        }

        private void SetDifficulty(Difficulty newDifficulty)
        {
            DifficultyLevel = newDifficulty;
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