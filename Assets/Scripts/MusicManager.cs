using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource musicSource;
    public AudioSource effectSource;
    public AudioClip mainMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;

    public bool musicOn = true;

    void Awake()
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

        musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        musicSource.mute = !musicOn;
    }

    public void ToggleMusic()
    {
        musicOn = !musicOn;
        musicSource.mute = !musicOn;
        effectSource.mute = !musicOn;
        PlayerPrefs.SetInt("MusicOn", musicOn ? 1 : 0);
    }

    public bool IsMusicOn()
    {
        return musicOn;
    }

    public void PlayWin()
    {
        musicSource.Stop();
        if (musicOn)
            effectSource.PlayOneShot(winMusic);
    }

    public void PlayLose()
    {
        musicSource.Stop();
        if (musicOn)
            effectSource.PlayOneShot(loseMusic);
    }

    public void PlayMainMusic()
    {
        effectSource.Stop();
        musicSource.clip = mainMusic;
        musicSource.loop = true;
        if (musicOn)
            musicSource.Play();
    }
}