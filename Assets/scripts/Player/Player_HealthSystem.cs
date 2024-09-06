using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HealthSystem : MonoBehaviour
{

     [SerializeField]
    private int maxHP = 3;
    private int currentHP;
    private bool dealth = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage (int healthDamage){
        currentHP = currentHP - healthDamage;
        CheckDealth();
    }

    private void CheckDealth (){
        if (currentHP <= 0){
            dealth = true;
        }
    }
}
