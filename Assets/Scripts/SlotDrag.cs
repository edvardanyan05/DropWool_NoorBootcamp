using UnityEngine;
using Ray = UnityEngine.Ray;
using Plane = UnityEngine.Plane;
using Vector3 = UnityEngine.Vector3;

public class SlotDrag : MonoBehaviour
{
    public string slotColor;

    private bool isDragging = false;
    private Vector3 startPosition;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = mainCamera.ScreenPointToRay(touch.position);
            HandleInput(ray, touch.phase == TouchPhase.Began, touch.phase == TouchPhase.Moved, touch.phase == TouchPhase.Ended);
        }
        else
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            HandleInput(ray, Input.GetMouseButtonDown(0), Input.GetMouseButton(0), Input.GetMouseButtonUp(0));
        }
    }

    void HandleInput(Ray ray, bool began, bool moved, bool ended)
    {
        if (began)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                    isDragging = true;
            }
        }

        if (moved && isDragging)
        {
            Plane plane = new Plane(Vector3.back, startPosition);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 worldPosition = ray.GetPoint(distance);
                transform.position = new Vector3(worldPosition.x, worldPosition.y, startPosition.z);
            }
        }

        if (ended && isDragging)
        {
            isDragging = false;
            CheckMatch();
        }
    }

    void CheckMatch()
    {
        Collider[] nearby = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (var col in nearby)
        {
            if (col.CompareTag(slotColor + "Spool"))
            {
                SpoolManager.Instance.MatchSlotToSpool(this, col.GetComponent<Spool>());
                Destroy(gameObject);
                return;
            }
        }
        transform.position = startPosition;
    }
}