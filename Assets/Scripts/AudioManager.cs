using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("----------- Garso šaltinis ------------")] [SerializeField]
    private AudioSource musicSource;

    [Header("----------- Garso įrašas ------------")]
    public AudioClip backgroundMusic;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}