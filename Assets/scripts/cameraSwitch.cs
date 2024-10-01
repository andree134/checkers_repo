using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitch : MonoBehaviour
{
    public checkersBoard checkersBoard; 
    public Camera firstCamera;
    public Camera secondCamera;
    public bool inFirstPersonView;
    public bool ableToSwitchCamera = true;

    [SerializeField]
    private Player_Movement playerMovementScript; //set the Player_Movement script REF.

    // Start is called before the first frame update
    void Start()
    {
        secondCamera.enabled = false;
        inFirstPersonView = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(inFirstPersonView && !checkersBoard.IsPieceSelected())
            {
                SwitchCamera();
            }
            else if(!inFirstPersonView && ableToSwitchCamera){
                
                playerMovementScript.SetActorToCheckerLocation(); //Call the Set Actor Location function in the Player_Movement script.
                SwitchCamera();
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
