// GENERATED AUTOMATICALLY FROM 'Assets/01_SCRIPTS/_Player_Mvt/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Actions"",
            ""id"": ""daea080c-935e-4ee7-9d15-2636e332256a"",
            ""actions"": [
                {
                    ""name"": ""Place"",
                    ""type"": ""Button"",
                    ""id"": ""c24b9e4b-b49c-42d6-8c9b-a4c74d662dcc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sell"",
                    ""type"": ""Button"",
                    ""id"": ""f0857890-9501-4e53-ab41-fc90e3e57645"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""00e06b30-1f26-4b73-a643-3a9e3408e66c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch"",
                    ""type"": ""Button"",
                    ""id"": ""7c721ace-b416-41ed-a51c-519d480d73d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectLeft"",
                    ""type"": ""Button"",
                    ""id"": ""4989faf4-5d72-4041-9250-e876898658ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectRight"",
                    ""type"": ""Button"",
                    ""id"": ""400e6a6a-5744-4e79-b7b3-7ed20c9e6a5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseScroll"",
                    ""type"": ""Value"",
                    ""id"": ""338162cd-7306-4fd9-9c6c-7695c2a45305"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reward"",
                    ""type"": ""Button"",
                    ""id"": ""6b527345-9e15-41d6-8b38-f1f07d3ef9f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shop"",
                    ""type"": ""Button"",
                    ""id"": ""bcd756c0-6e8d-4396-b9fb-7a3e1ffb7a51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraMove"",
                    ""type"": ""Value"",
                    ""id"": ""4e103b3a-0218-4f0c-b36a-6967e8431bf7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""1d2eb5b6-051b-4dd3-be12-10639ce0af4d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test"",
                    ""type"": ""Button"",
                    ""id"": ""58dddae6-f3cb-427f-926f-6f7a48640b35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateRight"",
                    ""type"": ""Button"",
                    ""id"": ""bd69cd04-5c05-4484-a54b-173b8864f9ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateLeft"",
                    ""type"": ""Button"",
                    ""id"": ""3528e7c8-519d-4144-bdbd-4c85302c68b7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Echap"",
                    ""type"": ""Button"",
                    ""id"": ""22503504-2a5a-4645-a394-7d144aeba6c0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""880c02a9-6880-42db-9554-15fa72625481"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04476ced-a3e9-49b0-82d5-3bf89574dcd4"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0cd7e4af-1cd3-46da-898c-4a1e1af2e2ba"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f96cf0ef-7beb-473c-b372-412bd29f2213"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2d7da70-380e-433c-a7b4-179a03da7113"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""SelectLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5dd7de1-0b6b-4dee-8a21-b5fd919c6a14"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""SelectRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76133531-74e1-4d17-a6d9-050404ca0da1"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Reward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""VerticalKeyboard"",
                    ""id"": ""8baeefdd-2676-4837-9c69-143656209e15"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e7de7966-f203-49e0-924e-091641f99617"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2e1bfc0d-f4a5-4533-acdb-5790bf9d6450"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""VerticalGamepad"",
                    ""id"": ""35682c68-401e-462b-b85b-3c29777f6f81"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ca03d903-df06-4ee2-9d56-f239136f38ad"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e394d3b5-5fe8-4bf7-b77d-6f21f608c2f4"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1f06fb7e-12b1-4673-ae96-cd33a3cab2ea"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""c672bbf6-4748-4192-a42e-d787514cf1e2"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5fe8dbb6-7a72-4091-8981-9556fb44199e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""566efdb0-07b0-4046-ba67-31b1e20b7c7f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0ad61092-0438-4023-ab1b-c1d8b18cefc0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2dbdd1d7-a535-4318-af9d-2fad6435cd45"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""33ca0558-ac09-40f9-a69f-d5a8ab04707f"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MouseScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c94a27fa-a776-4d33-b7d5-aba219c9f250"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b8d8257-4ef5-463a-b8fe-771da5384a88"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""138a289a-c8e4-4d87-b88d-6baa302e3d52"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4502653b-a800-4561-8b93-b7c78b9157f6"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Echap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9aa94cd4-169d-49ee-bd61-66a2fbcbca2f"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Shop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Actions
        m_Actions = asset.FindActionMap("Actions", throwIfNotFound: true);
        m_Actions_Place = m_Actions.FindAction("Place", throwIfNotFound: true);
        m_Actions_Sell = m_Actions.FindAction("Sell", throwIfNotFound: true);
        m_Actions_Jump = m_Actions.FindAction("Jump", throwIfNotFound: true);
        m_Actions_Switch = m_Actions.FindAction("Switch", throwIfNotFound: true);
        m_Actions_SelectLeft = m_Actions.FindAction("SelectLeft", throwIfNotFound: true);
        m_Actions_SelectRight = m_Actions.FindAction("SelectRight", throwIfNotFound: true);
        m_Actions_MouseScroll = m_Actions.FindAction("MouseScroll", throwIfNotFound: true);
        m_Actions_Reward = m_Actions.FindAction("Reward", throwIfNotFound: true);
        m_Actions_Shop = m_Actions.FindAction("Shop", throwIfNotFound: true);
        m_Actions_CameraMove = m_Actions.FindAction("CameraMove", throwIfNotFound: true);
        m_Actions_Move = m_Actions.FindAction("Move", throwIfNotFound: true);
        m_Actions_Test = m_Actions.FindAction("Test", throwIfNotFound: true);
        m_Actions_RotateRight = m_Actions.FindAction("RotateRight", throwIfNotFound: true);
        m_Actions_RotateLeft = m_Actions.FindAction("RotateLeft", throwIfNotFound: true);
        m_Actions_Echap = m_Actions.FindAction("Echap", throwIfNotFound: true);
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

    // Actions
    private readonly InputActionMap m_Actions;
    private IActionsActions m_ActionsActionsCallbackInterface;
    private readonly InputAction m_Actions_Place;
    private readonly InputAction m_Actions_Sell;
    private readonly InputAction m_Actions_Jump;
    private readonly InputAction m_Actions_Switch;
    private readonly InputAction m_Actions_SelectLeft;
    private readonly InputAction m_Actions_SelectRight;
    private readonly InputAction m_Actions_MouseScroll;
    private readonly InputAction m_Actions_Reward;
    private readonly InputAction m_Actions_Shop;
    private readonly InputAction m_Actions_CameraMove;
    private readonly InputAction m_Actions_Move;
    private readonly InputAction m_Actions_Test;
    private readonly InputAction m_Actions_RotateRight;
    private readonly InputAction m_Actions_RotateLeft;
    private readonly InputAction m_Actions_Echap;
    public struct ActionsActions
    {
        private @Inputs m_Wrapper;
        public ActionsActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Place => m_Wrapper.m_Actions_Place;
        public InputAction @Sell => m_Wrapper.m_Actions_Sell;
        public InputAction @Jump => m_Wrapper.m_Actions_Jump;
        public InputAction @Switch => m_Wrapper.m_Actions_Switch;
        public InputAction @SelectLeft => m_Wrapper.m_Actions_SelectLeft;
        public InputAction @SelectRight => m_Wrapper.m_Actions_SelectRight;
        public InputAction @MouseScroll => m_Wrapper.m_Actions_MouseScroll;
        public InputAction @Reward => m_Wrapper.m_Actions_Reward;
        public InputAction @Shop => m_Wrapper.m_Actions_Shop;
        public InputAction @CameraMove => m_Wrapper.m_Actions_CameraMove;
        public InputAction @Move => m_Wrapper.m_Actions_Move;
        public InputAction @Test => m_Wrapper.m_Actions_Test;
        public InputAction @RotateRight => m_Wrapper.m_Actions_RotateRight;
        public InputAction @RotateLeft => m_Wrapper.m_Actions_RotateLeft;
        public InputAction @Echap => m_Wrapper.m_Actions_Echap;
        public InputActionMap Get() { return m_Wrapper.m_Actions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionsActions set) { return set.Get(); }
        public void SetCallbacks(IActionsActions instance)
        {
            if (m_Wrapper.m_ActionsActionsCallbackInterface != null)
            {
                @Place.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPlace;
                @Place.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPlace;
                @Place.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPlace;
                @Sell.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSell;
                @Sell.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSell;
                @Sell.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSell;
                @Jump.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnJump;
                @Switch.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitch;
                @Switch.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitch;
                @Switch.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitch;
                @SelectLeft.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSelectLeft;
                @SelectLeft.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSelectLeft;
                @SelectLeft.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSelectLeft;
                @SelectRight.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSelectRight;
                @SelectRight.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSelectRight;
                @SelectRight.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSelectRight;
                @MouseScroll.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMouseScroll;
                @MouseScroll.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMouseScroll;
                @MouseScroll.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMouseScroll;
                @Reward.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReward;
                @Reward.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReward;
                @Reward.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReward;
                @Shop.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnShop;
                @Shop.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnShop;
                @Shop.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnShop;
                @CameraMove.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnCameraMove;
                @CameraMove.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnCameraMove;
                @CameraMove.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnCameraMove;
                @Move.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMove;
                @Test.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnTest;
                @Test.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnTest;
                @Test.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnTest;
                @RotateRight.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateRight;
                @RotateRight.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateRight;
                @RotateRight.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateRight;
                @RotateLeft.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotateLeft;
                @Echap.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnEchap;
                @Echap.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnEchap;
                @Echap.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnEchap;
            }
            m_Wrapper.m_ActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Place.started += instance.OnPlace;
                @Place.performed += instance.OnPlace;
                @Place.canceled += instance.OnPlace;
                @Sell.started += instance.OnSell;
                @Sell.performed += instance.OnSell;
                @Sell.canceled += instance.OnSell;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Switch.started += instance.OnSwitch;
                @Switch.performed += instance.OnSwitch;
                @Switch.canceled += instance.OnSwitch;
                @SelectLeft.started += instance.OnSelectLeft;
                @SelectLeft.performed += instance.OnSelectLeft;
                @SelectLeft.canceled += instance.OnSelectLeft;
                @SelectRight.started += instance.OnSelectRight;
                @SelectRight.performed += instance.OnSelectRight;
                @SelectRight.canceled += instance.OnSelectRight;
                @MouseScroll.started += instance.OnMouseScroll;
                @MouseScroll.performed += instance.OnMouseScroll;
                @MouseScroll.canceled += instance.OnMouseScroll;
                @Reward.started += instance.OnReward;
                @Reward.performed += instance.OnReward;
                @Reward.canceled += instance.OnReward;
                @Shop.started += instance.OnShop;
                @Shop.performed += instance.OnShop;
                @Shop.canceled += instance.OnShop;
                @CameraMove.started += instance.OnCameraMove;
                @CameraMove.performed += instance.OnCameraMove;
                @CameraMove.canceled += instance.OnCameraMove;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Test.started += instance.OnTest;
                @Test.performed += instance.OnTest;
                @Test.canceled += instance.OnTest;
                @RotateRight.started += instance.OnRotateRight;
                @RotateRight.performed += instance.OnRotateRight;
                @RotateRight.canceled += instance.OnRotateRight;
                @RotateLeft.started += instance.OnRotateLeft;
                @RotateLeft.performed += instance.OnRotateLeft;
                @RotateLeft.canceled += instance.OnRotateLeft;
                @Echap.started += instance.OnEchap;
                @Echap.performed += instance.OnEchap;
                @Echap.canceled += instance.OnEchap;
            }
        }
    }
    public ActionsActions @Actions => new ActionsActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IActionsActions
    {
        void OnPlace(InputAction.CallbackContext context);
        void OnSell(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSwitch(InputAction.CallbackContext context);
        void OnSelectLeft(InputAction.CallbackContext context);
        void OnSelectRight(InputAction.CallbackContext context);
        void OnMouseScroll(InputAction.CallbackContext context);
        void OnReward(InputAction.CallbackContext context);
        void OnShop(InputAction.CallbackContext context);
        void OnCameraMove(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnTest(InputAction.CallbackContext context);
        void OnRotateRight(InputAction.CallbackContext context);
        void OnRotateLeft(InputAction.CallbackContext context);
        void OnEchap(InputAction.CallbackContext context);
    }
}
