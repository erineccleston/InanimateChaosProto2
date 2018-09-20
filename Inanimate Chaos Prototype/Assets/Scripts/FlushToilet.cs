using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlushToilet : MonoBehaviour {

    public ClogChecker clog;
    public Text Win;

    void Start()
    {
        Win.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Flushed the toilet!");
        if (clog.isClogged) {
            Debug.Log("You win!");
        }

        Time.timeScale = 0;
        Win.enabled = true;

    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Not Flushing anymore.");
    }
}
