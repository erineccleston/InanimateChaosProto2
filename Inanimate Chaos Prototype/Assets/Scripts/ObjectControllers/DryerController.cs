using UnityEngine;

public class DryerController : PlayerController
{
    public float Thrust = 10;
    public float Lift = 1.5f;
    public float Charge = 10;

    public Transform ThrustPosition;
    public AudioSource sound;

    new void Start()
    {
        base.Start();

        if (ThrustPosition == null)
            ThrustPosition = transform;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (Charge > 0 && Input.GetKey(KeyCode.Space))
        {
            if (!sound.isPlaying)
                sound.Play();
            Charge -= Time.fixedDeltaTime;
            rb.AddForce(Vector3.up * Lift);
            rb.AddForceAtPosition(Thrust * transform.right, ThrustPosition.position + Vector3.down * .1f);
        }

        else
        {
            sound.Stop();
        }
    }
}
