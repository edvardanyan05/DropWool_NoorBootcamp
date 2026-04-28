using UnityEngine;

public class LevelPanelOpener : MonoBehaviour
{
    public GameObject levelPanel;
    private bool isPanelOpen = false;

    public void LevelButtonClicked()
    {
        if (!isPanelOpen)
        {
            levelPanel.SetActive(true);
            isPanelOpen = true;
        }else
        {
            levelPanel.SetActive(false);
            isPanelOpen = false;
        }
    }
}
