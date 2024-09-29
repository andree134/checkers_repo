using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float timer;
    private bool paused = false;
    public float timerLength;

    private void Awake()
    {
        timer = timerLength;
    }

    private void FixedUpdate()
    {
        if(!paused)
        timer = timer - Time.fixedDeltaTime; 
    }

    public void ResetTimer()
    {
        timer = timerLength;
    }

    public float GetTimer()
    {
        return timer; 
    }

    public void Paused(bool isPaused)
    {
        paused = isPaused;
    }
}
