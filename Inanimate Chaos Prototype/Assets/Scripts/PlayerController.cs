using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10;
    public float Thrust = 10;
    public float Lift = 1.5f;
    public float Charge = 10;

    public Transform ThrustPosition;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Charge > 0 && Input.GetKey(KeyCode.Space))
        {
            Charge -= Time.fixedDeltaTime;
            rb.AddForce(Vector3.up * Lift);
            rb.AddForceAtPosition(Thrust * transform.right, ThrustPosition.position + Vector3.down * .1f);
        }

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * Speed);
    }
}