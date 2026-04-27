using UnityEngine;

public class Spool : MonoBehaviour
{
    public string spoolColor;
    public GameObject thread;

    public void ShowThread()
    {
        if (thread != null)
            thread.SetActive(true);
    }
}