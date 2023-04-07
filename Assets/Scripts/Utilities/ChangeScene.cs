using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tetris.Utilities
{
    public class ChangeScene : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}