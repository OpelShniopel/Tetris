using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("----------- Garso šaltinis ------------")]
    [SerializeField] AudioSource musicSource;

    [Header("----------- Garso įrašas ------------")]
    public AudioClip backgroundMusic;

    public static AudioManager instance;


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
    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();

    }

    // Update is called once per frame
    void Update()
    {

    }
}