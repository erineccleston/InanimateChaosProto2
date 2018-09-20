using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlushToilet : MonoBehaviour {

    public ClogChecker clog;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Flushed the toilet!");
        if (clog.isClogged) {
            Debug.Log("You win!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Not Flushing anymore.");
    }
}
