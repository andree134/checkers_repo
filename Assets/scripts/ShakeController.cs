using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeController : MonoBehaviour
{
    [SerializeField] private ScreenShake[] shakers;
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

    private void Update()
    {
        if (shakers.Length < 4)
        {
            shakers = (ScreenShake[])FindObjectsOfType(typeof(ScreenShake));

            foreach (ScreenShake shaker in shakers)
            {
                shaker.SetDuration(duration);
                shaker.SetCurve(curve);
            }
        }
    }
}
