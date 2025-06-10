using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip backgroundMusic;

    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
}