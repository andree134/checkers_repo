using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    public void Play_Run (float speed){
        anim.SetFloat("Speed", speed);
    }

    public void Play_MovePiece (bool isMovingPiece){
        anim.SetBool("IsMovingPiece", isMovingPiece);
    }

    public void Play_State (int characterState){
        anim.SetInteger("CharacterState", characterState);
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

