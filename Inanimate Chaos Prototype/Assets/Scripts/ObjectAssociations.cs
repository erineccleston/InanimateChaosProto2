#pragma warning disable 660, 661

using System;
using UnityEngine;

public class ObjectAssociations : ScriptableObject
{
    [Serializable]
    public struct Pair
    {
        public GameObject Model;
#if UNITY_EDITOR
        public UnityEditor.MonoScript Controller;
#endif

        [HideInInspector]
        public Mesh Mesh;
        public Type Script;
        [HideInInspector]
        public string ScriptName;

        public PhysicsData PhysicsData;

        public static bool operator ==(Pair p, Mesh m)
        {
            return p.Mesh == m;
        }

        public static bool operator !=(Pair p, Mesh m)
        {
            return p.Mesh != m;
        }
    }

    [Serializable]
    public struct PhysicsData
    {
        public float Mass;
        public float Drag;
        public float AngularDrag;

        public float Speed;
    }

    public Pair[] Associations = new Pair[0];

    void OnValidate()
    {
        for (int i = 0; i < Associations.Length; i++)
        {
            var model = Associations[i].Model;
            if (model)
            {
                var mf = model.GetComponentInChildren<MeshFilter>();
                if (mf)
                    Associations[i].Mesh = mf.sharedMesh;
                else
                {
                    Associations[i].Model = null;
                    Associations[i].Mesh = null;
                }
            }
            else
                Associations[i].Mesh = null;

#if UNITY_EDITOR
            var controller = Associations[i].Controller;
            if (controller)
            {
                if (controller.GetClass().IsSubclassOf(typeof(PlayerController)))
                {
                    Associations[i].Script = controller.GetClass();
                    Associations[i].ScriptName = controller.GetClass().AssemblyQualifiedName;
                }
                else
                {
                    Associations[i].Controller = null;
                    Associations[i].Script = null;
                    Associations[i].ScriptName = null;
                }
            }
            else
            {
                Associations[i].Script = null;
                Associations[i].ScriptName = null;
            }
#endif

            var data = Associations[i].PhysicsData;
            if (data.Mass + data.Drag + data.AngularDrag + data.Speed == 0)
            {
                Associations[i].PhysicsData.Mass = 1;
                Associations[i].PhysicsData.Drag = 0;
                Associations[i].PhysicsData.AngularDrag = .05f;
                Associations[i].PhysicsData.Speed = 10;
            }
        }
    }
}
