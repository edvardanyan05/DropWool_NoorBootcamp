using UnityEngine;

public class SpoolManager : MonoBehaviour
{
    public static SpoolManager Instance;
    public Transform[] topSlots;

    void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
    }

    public void MatchSlotToSpool(SlotDrag slot, Spool spool)
    {    
        Transform freeSlot = null;
        foreach (Transform t in topSlots)
        {
            if (t.childCount == 0)
            {
                freeSlot = t;
                break;
            }
        }

        if (freeSlot == null)
        {
            return;
        }
        spool.transform.SetParent(freeSlot);
        spool.transform.position = freeSlot.position;
    }
}