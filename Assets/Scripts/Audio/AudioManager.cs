using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tetris.Audio
{
    
    public class AudioManager : MonoBehaviour
    {
     /// <summary>
     /// The clip to play outside menus.
     /// </summary>
        [SerializeField]
        private AudioClip levelMusic;
 
        [SerializeField]
     /// <summary>
     /// The component that plays the music
     /// </summary>
        private AudioSource source;
 
     /// <summary>
     /// This class follows the singleton pattern and this is its instance
     /// </summary>
        static private AudioManager instance;
 
     /// <summary>
     /// Awake is not public because other scripts have no reason to call it directly,
     /// only the Unity runtime does (and it can call protected and private methods).
     /// It is protected virtual so that possible subclasses may perform more specific
     /// tasks in their own Awake and still call this base method (It's like constructors
     /// in object-oriented languages but compatible with Unity's component-based stuff.
     /// </summary>
        protected virtual void Awake() {
         // Singleton enforcement
            if (instance == null) {
             // Register as singleton if first
                instance = this;
                DontDestroyOnLoad(this);
            } else if(instance != this)
            {
                instance.source.Stop();
                instance = this;
                Destroy(instance);
                PlayGameMusic();
            }
        }

        //protected virtual void Start()
        //{
        //    PlayGameMusic();
        //}
     
     /// <summary>
     /// Plays the music designed for outside menus
     /// This method is static so that it can be called from anywhere in the code.
     /// </summary>
        static public void PlayGameMusic ()
        {
            if (instance != null) {
                if (instance.source != null) {
                        instance.source.Stop();
                        instance.source.clip = instance.levelMusic;
                        instance.source.Play();
                }
            }
        }
    }
}