using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitch : MonoBehaviour
{
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
            if(inFirstPersonView){
                SwitchCamera();
            }
            else if(!inFirstPersonView && ableToSwitchCamera){
                SwitchCamera();
                playerMovementScript.SetActorToCheckerLocation(); //Call the Set Actor Location function in the Player_Movement script.
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
