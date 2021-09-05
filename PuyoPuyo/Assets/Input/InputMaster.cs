// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""GameplayActions"",
            ""id"": ""cdef8ff4-0e11-4fce-916f-1ab6ff09cbf0"",
            ""actions"": [
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""25a98078-469b-4fc6-a1c1-7509671baebf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""98a38663-2858-4b81-a417-0fa9a7198313"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""51cf5226-0a71-4604-b63c-ab2d6ca752a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""a573ede9-cd7a-494d-8d57-6b97b58d62da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f52bf9dc-3136-4336-80c7-e285b055e7f4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c0858be-b586-45d9-a224-f1e54bea5eaf"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f06e54a-8eed-4474-b89f-0918ceb4c2d6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70ee3f56-6f59-41dc-b14b-0e8027d14129"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GameplayActions
        m_GameplayActions = asset.FindActionMap("GameplayActions", throwIfNotFound: true);
        m_GameplayActions_MoveLeft = m_GameplayActions.FindAction("MoveLeft", throwIfNotFound: true);
        m_GameplayActions_MoveRight = m_GameplayActions.FindAction("MoveRight", throwIfNotFound: true);
        m_GameplayActions_MoveDown = m_GameplayActions.FindAction("MoveDown", throwIfNotFound: true);
        m_GameplayActions_Rotate = m_GameplayActions.FindAction("Rotate", throwIfNotFound: true);
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

    // GameplayActions
    private readonly InputActionMap m_GameplayActions;
    private IGameplayActionsActions m_GameplayActionsActionsCallbackInterface;
    private readonly InputAction m_GameplayActions_MoveLeft;
    private readonly InputAction m_GameplayActions_MoveRight;
    private readonly InputAction m_GameplayActions_MoveDown;
    private readonly InputAction m_GameplayActions_Rotate;
    public struct GameplayActionsActions
    {
        private @InputMaster m_Wrapper;
        public GameplayActionsActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveLeft => m_Wrapper.m_GameplayActions_MoveLeft;
        public InputAction @MoveRight => m_Wrapper.m_GameplayActions_MoveRight;
        public InputAction @MoveDown => m_Wrapper.m_GameplayActions_MoveDown;
        public InputAction @Rotate => m_Wrapper.m_GameplayActions_Rotate;
        public InputActionMap Get() { return m_Wrapper.m_GameplayActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActionsActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActionsActions instance)
        {
            if (m_Wrapper.m_GameplayActionsActionsCallbackInterface != null)
            {
                @MoveLeft.started -= m_Wrapper.m_GameplayActionsActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_GameplayActionsActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_GameplayActionsActionsCallbackInterface.OnMoveLeft;
                @MoveRight.started -= m_Wrapper.m_GameplayActionsActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_GameplayActionsActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_GameplayActionsActionsCallbackInterface.OnMoveRight;
                @MoveDown.started -= m_Wrapper.m_GameplayActionsActionsCallbackInterface.OnMoveDown;
                @MoveDown.performed -= m_Wrapper.m_GameplayActionsActionsCallbackInterface.OnMoveDown;
                @MoveDown.canceled -= m_Wrapper.m_GameplayActionsActionsCallbackInterface.OnMoveDown;
                @Rotate.started -= m_Wrapper.m_GameplayActionsActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_GameplayActionsActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_GameplayActionsActionsCallbackInterface.OnRotate;
            }
            m_Wrapper.m_GameplayActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @MoveDown.started += instance.OnMoveDown;
                @MoveDown.performed += instance.OnMoveDown;
                @MoveDown.canceled += instance.OnMoveDown;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
            }
        }
    }
    public GameplayActionsActions @GameplayActions => new GameplayActionsActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IGameplayActionsActions
    {
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveDown(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
    }
}
