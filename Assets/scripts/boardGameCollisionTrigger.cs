using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardGameCollisionTrigger : MonoBehaviour
{
    private cameraSwitch handlingPlayer;

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            handlingPlayer = other.gameObject.GetComponent<cameraSwitch>();
            handlingPlayer. ableToSwitchCamera = true;
            Debug.Log("Colide");
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            Debug.Log("No Col");
            handlingPlayer = other.gameObject.GetComponent<cameraSwitch>();
            handlingPlayer. ableToSwitchCamera = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
