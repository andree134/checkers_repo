using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.InputSystem;

public class CursorMovement : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    float horizontalInput;
    float verticalInput;
    cameraSwitch canMoveCursor;

    [SerializeField] bool isWhitePlayer;

    [SerializeField] PlayerInput playerInput;


    private void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canMoveCursor = FindObjectOfType<cameraSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMoveCursor.inFirstPersonView)
        {
           Inputs();            
        }
        transform.Translate(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime, 0f);
    }
    void Inputs()
    {
        if (isWhitePlayer)
        {
            //horizontalInput = Input.GetAxis("P1Horizontal");
            //verticalInput = Input.GetAxis("P1Vertical");

            horizontalInput = playerInput.actions["Move"].ReadValue<Vector2>().x;
            verticalInput = playerInput.actions["Move"].ReadValue<Vector2>().y;
        }
        else
        {
            //horizontalInput = Input.GetAxis("P2Horizontal");
            //verticalInput = Input.GetAxis("P2Vertical");

            horizontalInput = playerInput.actions["Move"].ReadValue<Vector2>().x;
            verticalInput = playerInput.actions["Move"].ReadValue<Vector2>().y;
        }
    }
}
