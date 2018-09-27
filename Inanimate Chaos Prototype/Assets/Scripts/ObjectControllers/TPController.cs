using UnityEngine;

public class TPController : PlayerController
{
    public float JumpPower = 120;

    private bool Jumping;

    new void Start()
    {
        base.Start();
    }

    void Update()
    {
        Jumping = Input.GetKeyDown(KeyCode.Space);
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (Jumping && Mathf.Abs(rb.velocity.y) < .1f)
        {
            rb.AddForce(Vector3.up * JumpPower);
            Jumping = true;
        }
    }
}
