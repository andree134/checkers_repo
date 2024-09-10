using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingPlayers : MonoBehaviour
{
    [SerializeField]
    private bool isDeathZone;
    private Player_HealthSystem player;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){

            player = other.gameObject.GetComponent<Player_HealthSystem>();

            if(isDeathZone==true){
             player.TakeDamage(3);
             Debug.Log("Player is death");
            }
            else{
            player.TakeDamage(1);
            Debug.Log("Player get hitted");
            }

        }

        else if (other.CompareTag("LevelGround")){

            //play FXs.
            Debug.Log("Land On Ground.");
            Destroy(gameObject);
        }
    }
}
