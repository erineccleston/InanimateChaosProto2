using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10;

    protected Rigidbody rb;

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * Speed);
    }
}