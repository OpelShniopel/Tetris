using UnityEngine;

namespace Tetris.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public string CurrentMode { get; private set; }

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
                SetGameMode(CurrentMode);
            }
        }

        public void SetGameMode(string mode)
        {
            CurrentMode = mode;
        }
    }
}