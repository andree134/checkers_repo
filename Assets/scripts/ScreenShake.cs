using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public bool start = false; //debugging only
    private AnimationCurve curve;
    private float duration;

    private void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    public void Shake()
    {
        StartCoroutine(Shaking());
    }

    public void SetDuration(float newDuration)
    {
        duration = newDuration;
    }

    public void SetCurve(AnimationCurve newCurve)
    {
        curve = newCurve;
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elaspsedTime = 0f;

        while (elaspsedTime < duration)
        {
            elaspsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elaspsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }
}
