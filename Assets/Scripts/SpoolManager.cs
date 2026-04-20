using UnityEngine;

public class SpoolManager : MonoBehaviour
{
    public static SpoolManager Instance;

    public Transform[] TopSlots;

    void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
    }

    public void MatchSlotToSpool(SlotDrag slot, Spool spool)
    {
        
    }
}
