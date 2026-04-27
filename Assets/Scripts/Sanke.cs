using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    public static Snake Instance;

    public List<GameObject> bodyParts = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    public string GetNextBodyColor()
    {
        if (bodyParts.Count == 0) return "";
        return bodyParts[0].tag;
    }

    public void RemoveFirstBody()
    {
        if (bodyParts.Count == 0) return;

        GameObject body = bodyParts[0];
        bodyParts.RemoveAt(0);
        Destroy(body);

        if (bodyParts.Count == 0)
        {
            WinPanel.Instance.ShowWin();
        }
    }
}