using UnityEngine;

public class LosePanel : MonoBehaviour
{
    public static LosePanel Instance;

    public GameObject losePanel;

    void Awake()
    {
        Instance = this;
        losePanel.SetActive(false);
    }

    public void ShowLose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
        MusicManager.instance.PlayLose();
    }
}