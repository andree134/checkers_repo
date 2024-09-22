using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance; // Singleton instance for easy access

    private Controller inputActions;
    private checkersBoard checkerBoard;

    public event Action<Vector2> OnMove; // Event for movement (Checkerboard/Third-Person)
    public event Action OnAction; // Event for generic action (Select/Jump/Interact)
    public event Action OnModeSwitch; // Event for mode switch

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize input actions
        inputActions = new Controller();
        checkerBoard = new checkersBoard();

        // Subscribe to input events
        inputActions.Checkers.Move.performed += ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
        inputActions.Checkers.Action.performed += ctx => OnAction?.Invoke();
        inputActions.Checkers.ModeSwitch.performed += ctx => OnModeSwitch?.Invoke();

        // Enable the input actions
        inputActions.Enable();
    }

    private void OnDestroy()
    {
        // Unsubscribe from input events
        inputActions.Checkers.Move.performed -= ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
        inputActions.Checkers.Action.performed -= ctx => OnAction?.Invoke();
        inputActions.Checkers.ModeSwitch.performed -= ctx => OnModeSwitch?.Invoke();

        // Disable the input actions
        inputActions.Disable();
    }
}
