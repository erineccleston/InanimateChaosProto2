using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Pivot;
    public GameObject FollowObject;
    public float FollowDistance = 1;
    public Vector2 FollowRange = new Vector2(1, 5);
    public Texture Reticle;

    public bool InSelectMode = false;

    void Start()
    {
        FollowObject.GetComponent<PlayerController>().enabled = true;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            InSelectMode = !InSelectMode;

        if (!InSelectMode)
        {
            FollowObject.SetActive(true);

            FollowDistance = Mathf.Clamp(FollowDistance - Input.GetAxis("Mouse ScrollWheel"), FollowRange.x, FollowRange.y);
            Pivot.transform.position = FollowObject.transform.position;

            Pivot.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
            transform.localPosition = new Vector3(0, 0, -FollowDistance);
            transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            FollowObject.SetActive(false);

            Pivot.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
            transform.localPosition = Vector3.zero;

            //transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

            RaycastHit hit;
            bool validHit = Physics.Raycast(transform.position, transform.forward, out hit);
            validHit = validHit && (hit.transform.GetComponent<Rigidbody>() != null);

            if (Input.GetKeyDown(KeyCode.Mouse0) && validHit)
            {
                InSelectMode = false;
                SwapControl(FollowObject, hit.transform.gameObject);
                FollowObject = hit.transform.gameObject;
            }
        }
    }

    void SwapControl(GameObject oldGO, GameObject newGO)
    {
        oldGO.SetActive(true);

        var pc = oldGO.GetComponent<PlayerController>();
        if (pc)
            pc.enabled = false;

        pc = newGO.GetComponent<PlayerController>();
        if (pc)
            pc.enabled = true;
        else
            newGO.AddComponent<PlayerController>();
    }

    void OnGUI()
    {
        if (InSelectMode)
            GUI.DrawTexture(new Rect((Screen.width - Reticle.width) / 2, (Screen.height - Reticle.height) / 2, Reticle.width, Reticle.height), Reticle);
    }
}
