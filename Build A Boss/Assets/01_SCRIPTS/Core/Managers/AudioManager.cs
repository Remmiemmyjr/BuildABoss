using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public AudioClip currSong;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySong(AudioClip _song)
    {
        audioSource.Stop();
        currSong = _song;
        audioSource.clip = currSong;
        audioSource.Play();
    }

    public void StopSong()
    {
        audioSource.Stop();
        currSong = null;
        audioSource.clip = currSong;
    }
}
