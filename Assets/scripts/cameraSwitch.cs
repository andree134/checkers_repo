using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class cameraSwitch : MonoBehaviour
{
    public checkersBoard checkersBoard; 
    public Camera firstCamera;
    public Camera secondCamera;
    public bool inFirstPersonView;
    public bool ableToSwitchCamera = true;
    public GameObject cursor; 

    [SerializeField]
    private Player_Movement playerMovementScript; //set the Player_Movement script REF.

    [SerializeField] private PlayerInput playerInput;

    private void OnValidate()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        checkersBoard = GameObject.Find("Plane").GetComponent<checkersBoard>();
        secondCamera.enabled = false;
        inFirstPersonView = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerInput.actions["ModeSwitch"].WasPressedThisFrame()) 
        {
            if(inFirstPersonView && !checkersBoard.IsPieceSelected())
            {
                SwitchCamera();
                cursor.SetActive(false); 
            }
            else if(!inFirstPersonView && ableToSwitchCamera){
                
                playerMovementScript.SetActorToCheckerLocation(); //Call the Set Actor Location function in the Player_Movement script.
                SwitchCamera();
                cursor.SetActive(true);
            }
        }
    }
    void SwitchCamera()
    {
        inFirstPersonView = !inFirstPersonView;
        firstCamera.enabled = !firstCamera.enabled;
        secondCamera.enabled = !secondCamera.enabled;
    }
}
