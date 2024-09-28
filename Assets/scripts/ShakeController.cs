using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeController : MonoBehaviour
{
    private ScreenShake[] shakers;
    public float duration = 2f;
    public AnimationCurve curve;

    void Start()
    {
        shakers = (ScreenShake[])FindObjectsOfType(typeof(ScreenShake));
        
        foreach(ScreenShake shaker in shakers)
        {
            shaker.SetDuration(duration);
            shaker.SetCurve(curve);
        }
    }

    public void StartShaking()
    {
        foreach (ScreenShake shaker in shakers)
        {
            shaker.Shake(); 
        }
    }
}
