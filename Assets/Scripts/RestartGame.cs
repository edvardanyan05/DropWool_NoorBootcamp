using UnityEngine;
using UnityEngine.SceneManagement; 

public class RestartGame : MonoBehaviour
{    public void RestartButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        MusicManager.instance.PlayMainMusic();
    }
}
