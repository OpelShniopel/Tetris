using UnityEngine;

namespace Tetris.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] [Header("----------- Garso šaltinis ------------")]
        private AudioSource musicSource;

        [SerializeField] [Header("----------- Garso įrašas ------------")]
        private AudioClip backgroundMusic;

        private static AudioManager Instance { get; set; }

        private AudioClip BackgroundMusic => backgroundMusic;


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

        // Start is called before the first frame update
        private void Start()
        {
            musicSource.clip = BackgroundMusic;
            musicSource.Play();
        }
    }
}