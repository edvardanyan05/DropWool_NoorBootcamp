using UnityEngine;

public class WinPanel : MonoBehaviour
{
    public static WinPanel Instance;

    public GameObject winPanel;

    void Awake()
    {
        Instance = this;
        winPanel.SetActive(false);
    }

    public void ShowWin()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
        MusicManager.instance.PlayWin();
    }
}
