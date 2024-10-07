using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip select;
    [SerializeField] private AudioClip next;
    [SerializeField] private AudioClip cancel;

    [SerializeField] private AudioSource audioSource;

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

    public void PlaySelestSFX()
    {
        audioSource.clip = select;
        audioSource.Play();
    }

    public void PlayNextSFX()
    {
        audioSource.clip = next;
        audioSource.Play();
    }

    public void PlayCancelSFX()
    {
        audioSource.clip = cancel;
        audioSource.Play();
    }

}
