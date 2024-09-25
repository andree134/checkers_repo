//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Controlers/Controller.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controller: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controller()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controller"",
    ""maps"": [
        {
            ""name"": ""Checkers"",
            ""id"": ""dac32f00-44e9-4d83-a1e7-d16b935123f0"",
            ""actions"": [
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""fdcaa73d-3c42-46fd-9c98-6c2187f7fc18"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""3791d2cd-2fb4-4f95-bef2-31b49b4c503c"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ModeSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""b11f3c01-8f91-47e5-a3de-0356dacd6b75"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cbeeb06b-f93e-45a5-905d-7fb0d4fdcf8c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34ec6707-7986-4194-89f3-bd046dedc96a"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18a66f3c-99b0-4e09-b01e-38146a373f9c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ModeSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""3rdPerson"",
            ""id"": ""7b89b859-c292-4b27-8b64-c0a2ad93702d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""c21c5194-ff72-406a-bb55-6caa4cea3486"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ModeSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""d2c9d700-cd7f-4cc4-bb90-b2b8f10dba81"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""06870d21-1c6a-44ca-8b1e-5e443c9aac59"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e576c71-a319-4ad0-ba83-1ddc31d534dc"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ModeSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Checkers
        m_Checkers = asset.FindActionMap("Checkers", throwIfNotFound: true);
        m_Checkers_Action = m_Checkers.FindAction("Action", throwIfNotFound: true);
        m_Checkers_Move = m_Checkers.FindAction("Move", throwIfNotFound: true);
        m_Checkers_ModeSwitch = m_Checkers.FindAction("ModeSwitch", throwIfNotFound: true);
        // 3rdPerson
        m__3rdPerson = asset.FindActionMap("3rdPerson", throwIfNotFound: true);
        m__3rdPerson_Move = m__3rdPerson.FindAction("Move", throwIfNotFound: true);
        m__3rdPerson_ModeSwitch = m__3rdPerson.FindAction("ModeSwitch", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Checkers
    private readonly InputActionMap m_Checkers;
    private List<ICheckersActions> m_CheckersActionsCallbackInterfaces = new List<ICheckersActions>();
    private readonly InputAction m_Checkers_Action;
    private readonly InputAction m_Checkers_Move;
    private readonly InputAction m_Checkers_ModeSwitch;
    public struct CheckersActions
    {
        private @Controller m_Wrapper;
        public CheckersActions(@Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @Action => m_Wrapper.m_Checkers_Action;
        public InputAction @Move => m_Wrapper.m_Checkers_Move;
        public InputAction @ModeSwitch => m_Wrapper.m_Checkers_ModeSwitch;
        public InputActionMap Get() { return m_Wrapper.m_Checkers; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheckersActions set) { return set.Get(); }
        public void AddCallbacks(ICheckersActions instance)
        {
            if (instance == null || m_Wrapper.m_CheckersActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CheckersActionsCallbackInterfaces.Add(instance);
            @Action.started += instance.OnAction;
            @Action.performed += instance.OnAction;
            @Action.canceled += instance.OnAction;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @ModeSwitch.started += instance.OnModeSwitch;
            @ModeSwitch.performed += instance.OnModeSwitch;
            @ModeSwitch.canceled += instance.OnModeSwitch;
        }

        private void UnregisterCallbacks(ICheckersActions instance)
        {
            @Action.started -= instance.OnAction;
            @Action.performed -= instance.OnAction;
            @Action.canceled -= instance.OnAction;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @ModeSwitch.started -= instance.OnModeSwitch;
            @ModeSwitch.performed -= instance.OnModeSwitch;
            @ModeSwitch.canceled -= instance.OnModeSwitch;
        }

        public void RemoveCallbacks(ICheckersActions instance)
        {
            if (m_Wrapper.m_CheckersActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICheckersActions instance)
        {
            foreach (var item in m_Wrapper.m_CheckersActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CheckersActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CheckersActions @Checkers => new CheckersActions(this);

    // 3rdPerson
    private readonly InputActionMap m__3rdPerson;
    private List<I_3rdPersonActions> m__3rdPersonActionsCallbackInterfaces = new List<I_3rdPersonActions>();
    private readonly InputAction m__3rdPerson_Move;
    private readonly InputAction m__3rdPerson_ModeSwitch;
    public struct _3rdPersonActions
    {
        private @Controller m_Wrapper;
        public _3rdPersonActions(@Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m__3rdPerson_Move;
        public InputAction @ModeSwitch => m_Wrapper.m__3rdPerson_ModeSwitch;
        public InputActionMap Get() { return m_Wrapper.m__3rdPerson; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(_3rdPersonActions set) { return set.Get(); }
        public void AddCallbacks(I_3rdPersonActions instance)
        {
            if (instance == null || m_Wrapper.m__3rdPersonActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m__3rdPersonActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @ModeSwitch.started += instance.OnModeSwitch;
            @ModeSwitch.performed += instance.OnModeSwitch;
            @ModeSwitch.canceled += instance.OnModeSwitch;
        }

        private void UnregisterCallbacks(I_3rdPersonActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @ModeSwitch.started -= instance.OnModeSwitch;
            @ModeSwitch.performed -= instance.OnModeSwitch;
            @ModeSwitch.canceled -= instance.OnModeSwitch;
        }

        public void RemoveCallbacks(I_3rdPersonActions instance)
        {
            if (m_Wrapper.m__3rdPersonActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(I_3rdPersonActions instance)
        {
            foreach (var item in m_Wrapper.m__3rdPersonActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m__3rdPersonActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public _3rdPersonActions @_3rdPerson => new _3rdPersonActions(this);
    public interface ICheckersActions
    {
        void OnAction(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnModeSwitch(InputAction.CallbackContext context);
    }
    public interface I_3rdPersonActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnModeSwitch(InputAction.CallbackContext context);
    }
}
