using UnityEngine;

public class MusicScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip MainTheme;
    public AudioClip GameOverTheme;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MainTheme;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverSet()
    {
        audioSource.Stop();
        audioSource.clip = GameOverTheme;
        audioSource.Play();
    }
}
