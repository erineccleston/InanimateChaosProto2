// http://wiki.unity3d.com/index.php/MouseOrbitImproved

using UnityEngine;

public class MouseOrbitImproved : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private new Rigidbody rigidbody;

    private float StartTime;
    private Vector3 OldPos;

    private bool InSelectMode;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (InSelectMode)
                SelectMode(false);
            else
                SelectMode(true);
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            if (OldPos != target.position)
            {
                OldPos = target.position;
                StartTime = Time.time;
            }

            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            if (!InSelectMode)
                distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            //RaycastHit hit;
            //if (Physics.Linecast(target.position, transform.position, out hit))
            //{
            //    distance -= hit.distance;
            //}
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;

            float t = Time.time - StartTime;
            transform.position = position;
            //Vector3.Lerp(transform.position, position, t);
            //new Vector3(Mathf.SmoothStep(transform.position.x, position.x, t),
            //Mathf.SmoothStep(transform.position.y, position.y, t),
            //Mathf.SmoothStep(transform.position.z, position.z, t));
        }
    }

    float _oldDistance;
    Transform _oldTarget;
    public void SelectMode(bool enter)
    {
        InSelectMode = enter;

        if (enter)
        {
            _oldDistance = distance;
            _oldTarget = target;

            target = new GameObject().transform;
            target.position = transform.position;
            distance = 0; // turning times by radius
        }
        else
        {
            distance = _oldDistance;
            GameObject.Destroy(target);
            target = _oldTarget;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}