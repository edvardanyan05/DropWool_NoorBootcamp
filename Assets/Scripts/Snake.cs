using UnityEngine;
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

        // позиция удаляемого сегмента
        Vector3 removedPos = bodyParts[0].transform.position;

        Destroy(bodyParts[0]);
        bodyParts.RemoveAt(0);

        // голова уходит назад на место удалённого сегмента
        transform.position = removedPos;

        if (bodyParts.Count == 0)
        {
            WinPanel.Instance.ShowWin();
        }
    }
}