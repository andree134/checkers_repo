using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DamagingPlayers : MonoBehaviour
{
    [SerializeField]
    private bool isDeathZone;
    private Player_HealthSystem playerHealth;

    private IEnumerator DisableCollider(Collider collider, float delay)
    {
        // Disable the collider
        collider.enabled = false;

        // Wait for x delay
        yield return new WaitForSeconds(delay);

        // Re-enable the collider
        collider.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {



            playerHealth = other.gameObject.GetComponent<Player_HealthSystem>();

            if (playerHealth.state == Player_HealthSystem.characterState.Idle)
            {
                if (isDeathZone == true)
                {
                    playerHealth.TakeDamage(3);
                    Debug.Log("Player is died");
                }
                else
                {
                    playerHealth.TakeDamage(1);
                    Debug.Log("Player get hitted");
                }
            }
            //StartCoroutine(DisableCollider(GetComponent<Collider>(), 2f));

        }

        else if (other.CompareTag("LevelGround"))
        {

            //play FXs.
            Debug.Log("Land On Ground.");
            Destroy(gameObject);
        }
    }
}
    
