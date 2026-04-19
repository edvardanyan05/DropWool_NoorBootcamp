using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    public GameObject musicOnIcon;
    public GameObject musicOffIcon;
    void OnEnable()
    {
        UpdateIcon();
    }
    public void Clicked()
    {
        MusicManager.instance.ToggleMusic();
        UpdateIcon();
    }
    void UpdateIcon()
    {
        if (MusicManager.instance == null) 
            return;

        bool musicOn = MusicManager.instance.IsMusicOn();
        musicOnIcon.SetActive(musicOn);
        musicOffIcon.SetActive(!musicOn);
    }


}
