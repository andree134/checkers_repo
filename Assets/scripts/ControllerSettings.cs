using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSettings : MonoBehaviour
{
    private bool isPlayer1Red; 
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SetPlayerRed(bool red)
    {
        isPlayer1Red = red; 
    }

    public bool IsPlayerRed()
    {
        return isPlayer1Red;
    }
}
