using UnityEngine;

namespace Tetris.Core
{
    public class DifficultyManager : MonoBehaviour
    {
        public static DifficultyManager Instance { get; private set; }
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

        public float GetStepDelay(int linesCleared)
        {
            float initialSpeed = 1f;
            float speedIncrease = 0.05f * linesCleared;
            const float minStepDelay = 0.05f;

            // Adjust initial speed and speed increase rate based on the difficulty level
            switch (DifficultyLevel)
            {
                case Difficulty.Easy:
                    initialSpeed = 1f;
                    speedIncrease = 0.05f * linesCleared;
                    break;
                case Difficulty.Medium:
                    initialSpeed = 0.6f;
                    speedIncrease = 0.05f * linesCleared;
                    break;
                case Difficulty.Hard:
                    initialSpeed = 0.2f;
                    speedIncrease = 0.05f * linesCleared;
                    break;
            }

            return Mathf.Max(initialSpeed - speedIncrease, minStepDelay);
        }
    }
}