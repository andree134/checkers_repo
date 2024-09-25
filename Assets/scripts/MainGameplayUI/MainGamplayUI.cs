using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainGamplayUI : MonoBehaviour
{
    [SerializeField] private Image player1Health1;
    [SerializeField] private Image player1Health2;
    [SerializeField] private Image player1Health3;
    [SerializeField] private Image player2Health1;
    [SerializeField] private Image player2Health2;
    [SerializeField] private Image player2Health3;

    [SerializeField] private GameObject whitePlayer;
    private Player_HealthSystem whitePlayerSystem;

    [SerializeField] private GameObject blackPlayer;
    private Player_HealthSystem blackPlayerSystem;


    // Start is called before the first frame update
    void Start()
    {
        whitePlayerSystem = whitePlayer.GetComponent<Player_HealthSystem>();
        blackPlayerSystem = blackPlayer.GetComponent<Player_HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBlackPlayerHealth();
        UpdateWhitePlayerHealth();
    }

    void UpdateWhitePlayerHealth(){
        switch(whitePlayerSystem.currentHP)
        {
            case 0:
                player1Health1.enabled = false;
                player1Health2.enabled = false;
                player1Health3.enabled = false;
                break;
            case 1:
                player1Health1.enabled = true;
                player1Health2.enabled = false;
                player1Health3.enabled = false;
                break;
            case 2:
                player1Health1.enabled = true;
                player1Health2.enabled = true;
                player1Health3.enabled = false;
                break;
            case 3:
                player1Health1.enabled = true;
                player1Health2.enabled = true;
                player1Health3.enabled = true;
                break;
        }
    }

    void UpdateBlackPlayerHealth(){
        switch(blackPlayerSystem.currentHP)
        {
            case 0:
                player2Health1.enabled = false;
                player2Health2.enabled = false;
                player2Health3.enabled = false;
                break;
            case 1:
                player2Health1.enabled = true;
                player2Health2.enabled = false;
                player2Health3.enabled = false;
                break;
            case 2:
                player2Health1.enabled = true;
                player2Health2.enabled = true;
                player2Health3.enabled = false;
                break;
            case 3:
                player2Health1.enabled = true;
                player2Health2.enabled = true;
                player2Health3.enabled = true;
                break;
        }
    }
}

   