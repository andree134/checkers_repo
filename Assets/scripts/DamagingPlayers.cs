using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DamagingPlayers : MonoBehaviour
{
    [SerializeField]
    private bool isDeathZone;
    private Player_HealthSystem playerHealth;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip falling;
    [SerializeField] private AudioClip hit;

    void Start()
    {
        audioSource.clip = falling;
        audioSource.pitch = UnityEngine.Random.Range(1.0f , 1.5f);
        audioSource.Play();
    }


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
        audioSource.Stop();
        audioSource.clip = hit;
        audioSource.pitch = UnityEngine.Random.Range(1.0f , 1.5f);
        audioSource.Play();

        if (other.CompareTag("Player"))
        {
            playerHealth = other.gameObject.GetComponent<Player_HealthSystem>();

            if (playerHealth.state == Player_HealthSystem.characterState.Idle && playerHealth.winState == Player_HealthSystem.winOrLose.Draw)
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

    }
}
    
