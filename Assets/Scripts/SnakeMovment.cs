using UnityEngine;

public class SnakeMovment : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 7f;

    void FixedUpdate()
    {
        rb.AddForce(Vector3.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chicken"))
        {
            LosePanel.Instance.ShowLose();
        }
    }
}