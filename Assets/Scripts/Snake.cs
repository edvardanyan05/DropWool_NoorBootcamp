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
        return bodyParts[1].tag;
    }

    public void RemoveFirstBody()
    {
        if (bodyParts.Count == 0) return;

        // позиция удаляемого сегмента
        Vector3 removedPos = bodyParts[1].transform.position;

        Destroy(bodyParts[1]);
        bodyParts.RemoveAt(1);

        // голова уходит назад на место удалённого сегмента
        bodyParts[0].transform.position -= 1f * Vector3.left;

        if (bodyParts.Count == 1)
        {
            WinPanel.Instance.ShowWin();
        }
    }
}