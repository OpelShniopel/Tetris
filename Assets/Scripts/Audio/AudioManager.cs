using UnityEngine;

namespace Tetris.Audio
{
    public sealed class AudioManager : MonoBehaviour
    {
        /// <summary>
        ///     The clip to play outside menus.
        /// </summary>
        [SerializeField] private AudioClip levelMusic;

        /// <summary>
        ///     The component that plays the music
        /// </summary>
        [SerializeField] private AudioSource source;

        /// <summary>
        ///     This class follows the singleton pattern and this is its instance
        /// </summary>
        private static AudioManager Instance { get; set; }

        /// <summary>
        ///     Awake is not public because other scripts have no reason to call it directly,
        ///     only the Unity runtime does (and it can call protected and private methods).
        ///     It is protected virtual so that possible subclasses may perform more specific
        ///     tasks in their own Awake and still call this base method (It's like constructors
        ///     in object-oriented languages but compatible with Unity's component-based stuff.
        /// </summary>
        private void Awake()
        {
            // Singleton enforcement
            if (Instance == null)
            {
                // Register as singleton if first
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (Instance != this)
            {
                Instance.source.Stop();
                Instance = this;
                Destroy(Instance);
                PlayGameMusic();
            }
        }

        /// <summary>
        ///     Plays the music designed for outside menus
        ///     This method is static so that it can be called from anywhere in the code.
        /// </summary>
        private static void PlayGameMusic()
        {
            if (Instance != null && Instance.source != null)
            {
                Instance.source.Stop();
                Instance.source.clip = Instance.levelMusic;
                Instance.source.Play();
            }
        }
    }
}