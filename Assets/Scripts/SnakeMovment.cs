using UnityEngine;

public class SnakeMovment : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5f;

    void FixedUpdate()
    {
        rb.AddForce(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chicken"))
        {
            LosePanel.Instance.ShowLose();
        }
    }
}