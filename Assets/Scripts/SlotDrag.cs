using UnityEngine;

public class SlotDrag : MonoBehaviour
{
    public string slotColor;
    public Collider dragArea;

    private Camera cam;
    private bool dragging;

    private Vector3 startPos;
    private float frontZ;

    void Start()
    {
        cam = Camera.main;
        startPos = transform.position;

        frontZ = startPos.z - 0.3f;

        OccupyCurrentCell();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            HandleInput(
                t.position,
                t.phase == TouchPhase.Began,
                t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary,
                t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled
            );
        }
        else
        {
            HandleInput(
                Input.mousePosition,
                Input.GetMouseButtonDown(0),
                Input.GetMouseButton(0),
                Input.GetMouseButtonUp(0)
            );
        }
    }

    void HandleInput(Vector3 screenPos, bool began, bool moved, bool ended)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);

        if (began)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    dragging = true;
                    FreeOldCell();
                }
            }
        }

        if (moved && dragging)
        {
            if (dragArea.Raycast(ray, out RaycastHit hit, 100f))
            {
                Vector3 p = hit.point;

                transform.position = new Vector3(
                    p.x,
                    p.y,
                    frontZ
                );
            }
        }

        if (ended && dragging)
        {
            dragging = false;
            CheckSpoolOrSnap();
        }
    }

    void CheckSpoolOrSnap()
    {
        Collider[] near = Physics.OverlapSphere(transform.position, 0.6f);

        foreach (var c in near)
        {
            Spool spool = c.GetComponent<Spool>();

            if (spool != null)
            {
                if (c.CompareTag(slotColor + "Spool"))
                {
                    if (SpoolManager.Instance.HasFreeSlot())
                    {
                        FreeOldCell();
                        SpoolManager.Instance.MatchSlotToSpool(this, spool);
                        Destroy(gameObject);
                        return;
                    }
                    else
                    {
                        ReturnBack();
                        return;
                    }
                }
                else
                {
                    ReturnBack();
                    return;
                }
            }
        }

        SnapToNearestCell();
    }

    void SnapToNearestCell()
    {
        GridCell[] cells = FindObjectsByType<GridCell>();

        GridCell nearest = null;
        float best = float.MaxValue;

        foreach (var c in cells)
        {
            float d = Vector2.Distance(
                new Vector2(transform.position.x, transform.position.y),
                new Vector2(c.transform.position.x, c.transform.position.y)
            );

            if (d < best)
            {
                best = d;
                nearest = c;
            }
        }

        if (nearest == null)
        {
            ReturnBack();
            return;
        }

        if (nearest.isOccupied && nearest.currentSlot != this)
        {
            ReturnBack();
            return;
        }

        nearest.isOccupied = true;
        nearest.currentSlot = this;

        Vector3 target = nearest.transform.position;
        target.z = frontZ;

        transform.position = target;
        startPos = target;
    }

    void ReturnBack()
    {
        transform.position = startPos;
        OccupyCurrentCell();
    }

    void OccupyCurrentCell()
    {
        GridCell[] cells = FindObjectsByType<GridCell>();

        foreach (var c in cells)
        {
            float d = Vector2.Distance(
                new Vector2(startPos.x, startPos.y),
                new Vector2(c.transform.position.x, c.transform.position.y)
            );

            if (d < 0.4f)
            {
                c.isOccupied = true;
                c.currentSlot = this;
                return;
            }
        }
    }

    void FreeOldCell()
    {
        GridCell[] cells = FindObjectsByType<GridCell>();

        foreach (var c in cells)
        {
            if (c.currentSlot == this)
            {
                c.currentSlot = null;
                c.isOccupied = false;
            }
        }
    }
}