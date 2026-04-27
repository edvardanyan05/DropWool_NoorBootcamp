using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void toMainMenu()
    {
        SceneManager.LoadScene(0);
        MusicManager.instance.PlayMainMusic();
    }
}
