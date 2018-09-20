using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClogChecker : MonoBehaviour {

    public bool isClogged = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " Clogged!");
        isClogged = true;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(gameObject.name + " Unclogged!");
        isClogged = false;
    }
}
