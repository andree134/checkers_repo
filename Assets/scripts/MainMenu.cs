using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
public void PlayGame ()
    { // Load the checkers game scene
        SceneManager.LoadScene("CheckersDefault");
    }

    public void LoadTutorial ()
    {
        // Load the tutorial game scene
        SceneManager.LoadScene("NarrativeAndTutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
