using UnityEngine;

public class ControllerAssociator : MonoBehaviour
{
    public ObjectAssociations OA;

    void Awake()
    {
        var mfs = FindObjectsOfType<MeshFilter>();

        foreach (var mf in mfs)
        {
            if (!mf.gameObject.GetComponent<Collider>())
            {
                var mc = mf.gameObject.AddComponent<MeshCollider>();
                mc.sharedMesh = mf.sharedMesh;
                mc.convex = true;
            }

            foreach (var pair in OA.Associations)
            {
                if (pair == mf.sharedMesh) // use string if this doesn't work
                {
                    if (!mf.gameObject.GetComponent<Rigidbody>())
                    {
                        var rb = mf.gameObject.AddComponent<Rigidbody>();
                        rb.mass = pair.PhysicsData.Mass;
                        rb.drag = pair.PhysicsData.Drag;
                        rb.angularDrag = pair.PhysicsData.AngularDrag;
                    }

                    //System.Type type = pair.Script ?? typeof(PlayerController);
                    string type = pair.ScriptName;
                    if (string.IsNullOrEmpty(type))
                        type = "PlayerController";
                    if (!mf.gameObject.GetComponent(type))
                    {
                        var pc = mf.gameObject.AddComponent(System.Type.GetType(type)) as PlayerController;
                        pc.enabled = false;
                        pc.Speed = pair.PhysicsData.Speed;
                    }
                }
            }
        }

        foreach (var pc in FindObjectsOfType<PlayerController>())
            pc.enabled = false;
    }
}
