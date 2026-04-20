using UnityEngine;

public class SnakeMovment : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5f;
    void FixedUpdate()
    {
        rb.AddForce(speed * Time.deltaTime, 0, 0);
    }
}
