using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private CharacterController characterController;

      [SerializeField] private float movementSpeed=5.0f;
      

      [Header("Gravity Options")]
      [SerializeField] [Range(-80.0f,0.0f)] private float gravityScale=-40.0f;

      [Header("Ground Options")]
      [SerializeField] private Transform groundCheckTransform;
      [SerializeField] private float groundCheckRadius=0.2f;
      [SerializeField] private LayerMask WhatIsGround;
      [SerializeField] private bool isWhitePlayer;

      private Vector3 movementDirection;
      private Vector3 verticalVelocity;

      private bool _isFalling;
      public bool IsFalling{ get {return _isFalling;} set {_isFalling=value;} }
      public bool IsGrounded = false;

      private Player_Animation playerAnim;

      [SerializeField]
      private GameObject model;
      [SerializeField]
      private Camera mainCamera;
      [SerializeField]
      private float rotateSpeed=7.5f;

      //private Character State;
      private Player_HealthSystem playerSystem;
      private cameraSwitch cameraControlScript;

    // Start is called before the first frame update
    void Awake()
    {
        characterController=GetComponent<CharacterController>();
        playerSystem = GetComponent<Player_HealthSystem>();
        cameraControlScript = GetComponent<cameraSwitch>();
        playerAnim=GetComponent<Player_Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        if(playerSystem.state == Player_HealthSystem.characterState.Idle && cameraControlScript.inFirstPersonView == false){
            HorizontalMovement();
        }
        
        VerticalMovement();
        AnimatePlayer();
    }

    void GroundCheck(){
       
        IsGrounded= 
        Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, WhatIsGround);

        if (IsGrounded==true)
            IsFalling=false;
        else
            IsFalling=true;

      }

      void HorizontalMovement(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput= Input.GetAxis("Vertical");

        movementDirection=new Vector3(horizontalInput, 0, verticalInput);
        movementDirection= transform.TransformDirection(movementDirection); //calculating movement input to world space

        if(playerSystem.state == Player_HealthSystem.characterState.Idle && cameraControlScript.inFirstPersonView == false){
          characterController.Move(movementDirection*movementSpeed*Time.deltaTime); //do the change of position
          RotateCharacter(horizontalInput, verticalInput);  //do the change of rotation
        } 
      }

      void RotateCharacter(float horizontalInput, float verticalInput){
        if(horizontalInput!=0||verticalInput!=0){
            transform.rotation=Quaternion.Euler(0.0f, mainCamera.transform.rotation.eulerAngles.y,0.0f);
            model.transform.rotation=Quaternion.Slerp(
                model.transform.rotation,
                Quaternion.LookRotation(new Vector3(movementDirection.x,0.0f,movementDirection.z)),
                rotateSpeed*Time.deltaTime);
        }
      }

      void VerticalMovement(){  //Gravity Bascially
        if(IsFalling==false && verticalVelocity.y<0.0f){
            verticalVelocity.y=0.0f;
        }
        else{
            verticalVelocity.y+=gravityScale * Time.deltaTime;
        }

        characterController.Move(verticalVelocity * Time.deltaTime);
      }

      public void SetActorToCheckerLocation(){
        if (isWhitePlayer == true){
          this.transform.position = new Vector3 (0.22f, 4.92f , -9.2f);
          this.transform.eulerAngles = new Vector3(0,0,0);
        }
        else {

        }
      }

      void AnimatePlayer(){
          playerAnim.Play_Run(Mathf.Abs(movementDirection.x)+Mathf.Abs(movementDirection.z));
      }
}
