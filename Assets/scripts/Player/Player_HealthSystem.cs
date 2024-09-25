using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HealthSystem : MonoBehaviour
{
    public enum characterState {Idle = 0, KnockDown = 1, Died = 2};

    [SerializeField]
    private int maxHP = 3;
    public int currentHP;

    [SerializeField]
    public characterState state = characterState.Idle;

    private Player_Animation playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim=GetComponent<Player_Animation>();
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatePlayer();
    }

     public void TakeDamage (int healthDamage){
        currentHP = currentHP - healthDamage;
        //Debug.Log("Player is damaged.");
        state = characterState.KnockDown;
        StartCoroutine(Recovering());
        CheckDealth();

    }

    IEnumerator Recovering(){
        yield return new WaitForSeconds(3.0f);
        if(state == characterState.KnockDown){
            state = characterState.Idle;
        }
    }

    private void CheckDealth (){
        if (currentHP <= 0){
            state = characterState.Died;
        }
    }

    void AnimatePlayer(){
        playerAnim.Play_State((int)state);
      }

}
