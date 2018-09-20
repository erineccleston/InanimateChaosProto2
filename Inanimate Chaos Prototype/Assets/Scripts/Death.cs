using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {

    public Camera TheCamera;
    public SuspicionMeter Meter;
    public Text GameOver;
    public bool DisableDeath = false;

	// Use this for initialization
	void Start ()
    {
        GameOver.enabled = false;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Meter.death && !DisableDeath)
        {
            Time.timeScale = 0;

            GameOver.enabled = true;

            //Application.Quit();


            //I'm still working on this part
            if (Input.GetKeyDown("enter"))
            {
                print(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }
        }
    }
}
