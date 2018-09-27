using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlushToilet : MonoBehaviour {

    public ClogChecker clog;
    public Text Win;
    public BoxCollider water;
    public SkinnedMeshRenderer ToiletWater;
    public AudioSource ToiletSound;
    public AudioSource FloodSound;


    void Start()
    {
        Win.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Flushed the toilet!");
        Debug.Log(clog.isClogged);
        if (clog.isClogged) {
            Debug.Log("You win!");
            water.GetComponent<MeshRenderer>().enabled = true;
            Win.enabled = true;
            ToiletSound.Play();
            FloodSound.PlayDelayed(6);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Not Flushing anymore.");
    }

    void Update()
    {
        if (Win.enabled)
        {
            Vector3 pos = water.transform.position;
            water.transform.position = new Vector3(pos.x, pos.y + .01f, pos.z);
            if (ToiletWater.GetBlendShapeWeight(0) > 1)
                ToiletWater.SetBlendShapeWeight(0, ToiletWater.GetBlendShapeWeight(0) - .5f);

        }
    }
}
