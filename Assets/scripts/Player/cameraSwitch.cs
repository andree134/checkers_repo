using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitch : MonoBehaviour
{
    public Camera firstCamera;
    public Camera secondCamera;

    // Start is called before the first frame update
    void Start()
    {
        secondCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCamera();
        }
    }
    void SwitchCamera()
    {
        firstCamera.enabled = !firstCamera.enabled;
        secondCamera.enabled = !secondCamera.enabled;
    }
}
