using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void OnEnable()
    {
        // Load the Scene in the Build Index called CheckersDefault
        SceneManager.LoadScene("CheckersDefault", LoadSceneMode.Single);
    }
}
