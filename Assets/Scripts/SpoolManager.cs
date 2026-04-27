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

    public bool HasFreeSlot()
    {
        foreach (Transform t in topSlots)
        {
            if (t.childCount == 0)
                return true;
        }
        return false;
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

        if (freeSlot == null) return;

        spool.transform.SetParent(freeSlot);
        spool.transform.localPosition = Vector3.zero;

        TryRemoveBody(spool);
    }

    void TryRemoveBody(Spool spool)
    {
        string nextColor = Snake.Instance.GetNextBodyColor();
        if (string.IsNullOrEmpty(nextColor)) return;

        foreach (Transform slot in topSlots)
        {
            if (slot == null || slot.childCount == 0) continue;

            Spool s = slot.GetComponentInChildren<Spool>();
            if (s == null) continue;

            if (s.spoolColor + "Body" == nextColor)
            {
                s.ShowThread();
                Snake.Instance.RemoveFirstBody();

                GameObject toDestroy = s.gameObject;
                slot.DetachChildren();
                Destroy(toDestroy, 1f);

                Invoke(nameof(TryRemoveBodyDelayed), 1.1f);
                return;
            }
        }
    }

    void TryRemoveBodyDelayed()
    {
        TryRemoveBody(null);
    }
}