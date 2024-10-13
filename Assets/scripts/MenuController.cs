using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [SerializeField] private TextMeshProUGUI player1Text;
    [SerializeField] private TextMeshProUGUI player2Text;

    [SerializeField] private ControllerSettings controllerSettings;

    PlayerInput playerInput1;
    PlayerInput playerInput2;

    InputDevice controller1;
    InputDevice controller2;

    [SerializeField] bool player1IsRed = false;
    [SerializeField] bool player2IsRed = false;
    [SerializeField] bool player1IsBlue = false;
    [SerializeField] bool player2IsBlue = false; 
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (controller1 == null)
        {
            controller1 = Gamepad.all[0];
        }

        if (controller2 == null)
        {
            controller2 = Gamepad.all[1];
        }

        if (playerInput1 == null)
        {
            if (controller1 != null)
                playerInput1 = PlayerInput.Instantiate(player1, playerIndex: 0, pairWithDevice: controller1);
        }

        if (playerInput2 == null)
        {
            if (controller2 != null)
                playerInput2 = PlayerInput.Instantiate(player2, playerIndex: 1, pairWithDevice: controller2);
        }

        if (playerInput1 != null) //dpad
        {
            if (playerInput1.GetDevice<Gamepad>().dpad.left.wasPressedThisFrame && !player2IsRed)
            {
                player1IsRed = true;
                player2IsBlue = true;
                controllerSettings.SetPlayerRed(player1IsRed);
            }

            if (playerInput1.GetDevice<Gamepad>().dpad.right.wasPressedThisFrame && !player2IsBlue)
            {
                player1IsBlue = true;
                player2IsRed = true;
                controllerSettings.SetPlayerRed(player1IsRed);
            }
        }


        if (playerInput2 != null) //dpad
        {
            if (playerInput2.GetDevice<Gamepad>().dpad.left.wasPressedThisFrame && !player1IsRed)
            {
                player2IsRed = true;
                player1IsBlue = true;
                controllerSettings.SetPlayerRed(player1IsRed);
            }

            if (playerInput2.GetDevice<Gamepad>().dpad.left.wasPressedThisFrame && !player1IsBlue)
            {
                player2IsBlue = true;
                player1IsRed = true;
                controllerSettings.SetPlayerRed(player1IsRed);
            }
        }

        if (player1IsRed)
        {
            player1Text.rectTransform.SetLocalPositionAndRotation(new Vector3(-972, 68, 0), new Quaternion());
            player2Text.rectTransform.SetLocalPositionAndRotation(new Vector3(-235, -108, 0), new Quaternion());
        }

        if (player2IsRed)
        {
            player1Text.rectTransform.SetLocalPositionAndRotation(new Vector3(-235, 68, 0), new Quaternion());
            player2Text.rectTransform.SetLocalPositionAndRotation(new Vector3(-972, -108, 0), new Quaternion());
        }
    }
}
