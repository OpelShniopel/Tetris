using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tetris
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu;
        public void Pause()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;

        }
        public void Resume()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;

        }
        public void Home(string sceneName)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);

        }
    }
}
