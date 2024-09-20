using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingPlayers : MonoBehaviour
{
    [SerializeField]
    private bool isDeathZone;
    private Player_HealthSystem playerHealth;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){



            playerHealth = other.gameObject.GetComponent<Player_HealthSystem>();

            if(playerHealth.state == Player_HealthSystem.characterState.Idle){
                if(isDeathZone==true){
                    playerHealth.TakeDamage(3);
                    Debug.Log("Player is died");
                }
                else{
                    playerHealth.TakeDamage(1);
                    Debug.Log("Player get hitted");
                }
            }

            

        }

        else if (other.CompareTag("LevelGround")){

            //play FXs.
            Debug.Log("Land On Ground.");
            Destroy(gameObject);
        }
    }
}
