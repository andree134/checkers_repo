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

    [SerializeField] private TextMeshProUGUI p1RedSidePos;
    [SerializeField] private TextMeshProUGUI p2RedSidePos;
    [SerializeField] private TextMeshProUGUI p1BlueSidePos;
    [SerializeField] private TextMeshProUGUI p2BlueSidePos;

    [SerializeField] private ControllerSettings controllerSettings;

    PlayerInput playerInput1;
    PlayerInput playerInput2;

    InputDevice controller1;
    InputDevice controller2;

    [SerializeField] bool player1IsRed = false;
    [SerializeField] bool player2IsRed = false;
    [SerializeField] bool player1IsBlue = false;
    [SerializeField] bool player2IsBlue = false;

    //Audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip select;

    //lerping
    private float lerpingTime = 0.2f;
    private float currentLerp = 0.3f;
    private float lerp;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (currentLerp < lerpingTime)
        {
            currentLerp = currentLerp + Time.deltaTime;
        }

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
            if (playerInput1.GetDevice<Gamepad>().dpad.left.wasPressedThisFrame)// && !player2IsRed)
            {
                currentLerp = 0.0f;
                player1IsRed = true;
                player1IsBlue = false;
                player2IsRed = false;
                player2IsBlue = true;
                controllerSettings.SetPlayerRed(player1IsRed);
                audioSource.clip = select;
                audioSource.Play();
            }

            if (playerInput1.GetDevice<Gamepad>().dpad.right.wasPressedThisFrame)// && !player2IsBlue)
            {
                currentLerp = 0.0f;
                player1IsRed = false;
                player1IsBlue = true;
                player2IsRed = true;
                player2IsBlue = false;
                controllerSettings.SetPlayerRed(player1IsRed);
                audioSource.clip = select;
                audioSource.Play();
            }
        }


        if (playerInput2 != null) //dpad
        {
            if (playerInput2.GetDevice<Gamepad>().dpad.left.wasPressedThisFrame)// && !player1IsRed)
            {
                currentLerp = 0.0f;
                player2IsRed = true;
                player2IsBlue = false;
                player1IsRed = false;
                player1IsBlue = true;
                controllerSettings.SetPlayerRed(player1IsRed);
                audioSource.clip = select;
                audioSource.Play();
            }

            if (playerInput2.GetDevice<Gamepad>().dpad.right.wasPressedThisFrame)// && !player1IsBlue)
            {
                currentLerp = 0.0f;
                player2IsRed = false;
                player2IsBlue = true;
                player1IsRed = true;
                player1IsBlue = false;
                controllerSettings.SetPlayerRed(player1IsRed);
                audioSource.clip = select;
                audioSource.Play();
            }
        }

        if (player1IsRed && currentLerp < lerpingTime)
        {
            lerp = currentLerp / lerpingTime;
            player1Text.rectTransform.position = Vector3.Lerp( player1Text.rectTransform.position, p1RedSidePos.rectTransform.position, lerp);
            player2Text.rectTransform.position = Vector3.Lerp( player2Text.rectTransform.position, p2BlueSidePos.rectTransform.position, lerp);
            //player1Text.rectTransform.SetLocalPositionAndRotation(new Vector3(-972, 68, 0), new Quaternion());
            //player2Text.rectTransform.SetLocalPositionAndRotation(new Vector3(-235, -108, 0), new Quaternion());
        }

        if (player2IsRed && currentLerp < lerpingTime)
        {
            lerp = currentLerp / lerpingTime;
            player1Text.rectTransform.position = Vector3.Lerp( player1Text.rectTransform.position, p1BlueSidePos.rectTransform.position, lerp);
            player2Text.rectTransform.position = Vector3.Lerp( player2Text.rectTransform.position, p2RedSidePos.rectTransform.position, lerp);
            //player1Text.rectTransform.SetLocalPositionAndRotation(new Vector3(-235, 68, 0), new Quaternion());
            //player2Text.rectTransform.SetLocalPositionAndRotation(new Vector3(-972, -108, 0), new Quaternion());
        }

        

        
    }
}


//audioSource.clip = select;
//audioSource.Play();