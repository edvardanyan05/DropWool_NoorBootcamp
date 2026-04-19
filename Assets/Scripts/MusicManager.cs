using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource musicSource;

    bool musicOn = true;

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
        PlayerPrefs.SetInt("MusicOn", musicOn ? 1 : 0);
    }

    public bool IsMusicOn()
    {
        return musicOn;
    }
}
