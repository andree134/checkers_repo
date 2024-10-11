using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardGameCollisionTrigger : MonoBehaviour
{
    private cameraSwitch handlingPlayer;
    [SerializeField] GameObject player1BoardViewText;
    [SerializeField] GameObject player2BoardViewText;

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            handlingPlayer = other.gameObject.GetComponent<cameraSwitch>();
            handlingPlayer. ableToSwitchCamera = true;
            //Debug.Log("Colide");
        }

        if (other.name == "Player")
        {
            player1BoardViewText.SetActive(true);
        }

        if (other.name == "Opponent(Clone)")
        {
            player2BoardViewText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            //Debug.Log("No Col");
            handlingPlayer = other.gameObject.GetComponent<cameraSwitch>();
            handlingPlayer. ableToSwitchCamera = false;
        }

        if (other.name == "Player")
        {
            player1BoardViewText.SetActive(false);
        }

        if (other.name == "Opponent(Clone)")
        {
            player2BoardViewText.SetActive(false);
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
