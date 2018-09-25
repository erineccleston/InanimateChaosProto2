using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {

    public Camera TheCamera;
    public bool IsDead = false;
    public bool DisableDeath = false;
    public RawImage GameOverScreen;
    public Button Restart;
    public Button Quit;

	// Use this for initialization
	void Start ()
    {
        GameOverScreen.enabled = false;
        Restart.enabled = false;
        Restart.image.enabled = false;
        Quit.enabled = false;
        Quit.image.enabled = false;

        Restart.onClick.AddListener(StartAgain);
        Quit.onClick.AddListener(QuitGame);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (IsDead && !DisableDeath)
        {
            GameOverScreen.enabled = true;
            Restart.enabled = true;
            Restart.image.enabled = true;
            Quit.enabled = true;
            Quit.image.enabled = true;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartAgain();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    void StartAgain()
    {
        SceneManager.LoadScene("StartMenu");
    }

    void QuitGame()
    {
        Application.Quit();

    }
}
