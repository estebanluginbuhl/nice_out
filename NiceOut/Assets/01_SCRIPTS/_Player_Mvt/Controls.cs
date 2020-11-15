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
                    ""name"": ""Refill"",
                    ""type"": ""Button"",
                    ""id"": ""08813f4e-b6f8-4639-8075-63112fda1f18"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fix"",
                    ""type"": ""Button"",
                    ""id"": ""cd075391-9a02-46ad-81f3-e5d9f5400b96"",
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
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""14db2cea-4f7d-4e2d-8353-761479834a02"",
                    ""expectedControlType"": """",
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
                    ""name"": ""Shop"",
                    ""type"": ""Button"",
                    ""id"": ""6b527345-9e15-41d6-8b38-f1f07d3ef9f5"",
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
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""9f206764-d9bb-498a-a970-a62799491f0e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""880c02a9-6880-42db-9554-15fa72625481"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab6cbce9-d324-4964-8e28-36d89c0cc5c4"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5230b43-1aa1-4012-a0fb-f29305471931"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Refill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69b99bdf-1604-4d9b-9b76-581557e09d3a"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Refill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04476ced-a3e9-49b0-82d5-3bf89574dcd4"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87467622-c69f-45b9-acbb-f1632cbcf9e8"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0cd7e4af-1cd3-46da-898c-4a1e1af2e2ba"",
                    ""path"": ""<Keyboard>/q"",
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
                    ""id"": ""55be5ffa-febc-4471-a976-7e08e0fbae0f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""id"": ""2bb4e1e1-92ee-4c78-90ab-e2a895e185b2"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""id"": ""85558579-e98f-48d5-bfb3-fbbe93a04e56"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SelectRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d47de52f-f97f-4d4c-a52d-3256a21549e2"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""PadScroll"",
                    ""id"": ""a5d563fc-94e1-4125-acc0-00427764c144"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4cbb8888-af1a-485e-83b5-41adca957b80"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""47cf1dfe-523e-4545-8c0a-0abfa9d9c51a"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""76133531-74e1-4d17-a6d9-050404ca0da1"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Shop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c3e2c63-1d99-4c2c-ad8e-13dfa0e53dce"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shop"",
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
                    ""name"": ""JoyStick"",
                    ""id"": ""50e5bf68-382a-44c8-9e40-e0e78307d1f5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=5,y=5)"",
                    ""groups"": """",
                    ""action"": ""CameraMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4eca6ba5-e781-4120-b131-ffc33b135c89"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""842b768d-86eb-4701-832d-f190bb3d4720"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c28e518a-2723-4788-ac45-ea5a69a3fa99"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""58e0305b-400a-4111-bac7-a4b8dd498c20"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CameraMove"",
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
                    ""name"": ""Gamepad"",
                    ""id"": ""d030dca1-3a1c-4bfd-b520-ad4613a86c8a"",
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
                    ""id"": ""a486b212-7cfe-4e3f-909b-8908a3c8a186"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=0.5,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ce5e92f3-7078-4406-8673-73fd2b3405ad"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=0.5,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8cbcadc3-5e16-4dbb-9ed4-d64e171262c9"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=0.5,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e620959d-8929-44d7-9413-62beb01950fe"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=0.5,max=1)"",
                    ""groups"": ""Gamepad"",
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
                    ""id"": ""8799d1ed-db2f-484b-9206-25c0ac64051b"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Fix"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ebfb4457-670b-4982-9618-0dc2df71fb31"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Fix"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84023068-d6a1-4ad5-ad16-4b0fef99d84e"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Rotate"",
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
        m_Actions_Refill = m_Actions.FindAction("Refill", throwIfNotFound: true);
        m_Actions_Fix = m_Actions.FindAction("Fix", throwIfNotFound: true);
        m_Actions_Sell = m_Actions.FindAction("Sell", throwIfNotFound: true);
        m_Actions_Jump = m_Actions.FindAction("Jump", throwIfNotFound: true);
        m_Actions_Switch = m_Actions.FindAction("Switch", throwIfNotFound: true);
        m_Actions_SelectLeft = m_Actions.FindAction("SelectLeft", throwIfNotFound: true);
        m_Actions_SelectRight = m_Actions.FindAction("SelectRight", throwIfNotFound: true);
        m_Actions_Scroll = m_Actions.FindAction("Scroll", throwIfNotFound: true);
        m_Actions_MouseScroll = m_Actions.FindAction("MouseScroll", throwIfNotFound: true);
        m_Actions_Shop = m_Actions.FindAction("Shop", throwIfNotFound: true);
        m_Actions_CameraMove = m_Actions.FindAction("CameraMove", throwIfNotFound: true);
        m_Actions_Move = m_Actions.FindAction("Move", throwIfNotFound: true);
        m_Actions_Rotate = m_Actions.FindAction("Rotate", throwIfNotFound: true);
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
    private readonly InputAction m_Actions_Refill;
    private readonly InputAction m_Actions_Fix;
    private readonly InputAction m_Actions_Sell;
    private readonly InputAction m_Actions_Jump;
    private readonly InputAction m_Actions_Switch;
    private readonly InputAction m_Actions_SelectLeft;
    private readonly InputAction m_Actions_SelectRight;
    private readonly InputAction m_Actions_Scroll;
    private readonly InputAction m_Actions_MouseScroll;
    private readonly InputAction m_Actions_Shop;
    private readonly InputAction m_Actions_CameraMove;
    private readonly InputAction m_Actions_Move;
    private readonly InputAction m_Actions_Rotate;
    public struct ActionsActions
    {
        private @Inputs m_Wrapper;
        public ActionsActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Place => m_Wrapper.m_Actions_Place;
        public InputAction @Refill => m_Wrapper.m_Actions_Refill;
        public InputAction @Fix => m_Wrapper.m_Actions_Fix;
        public InputAction @Sell => m_Wrapper.m_Actions_Sell;
        public InputAction @Jump => m_Wrapper.m_Actions_Jump;
        public InputAction @Switch => m_Wrapper.m_Actions_Switch;
        public InputAction @SelectLeft => m_Wrapper.m_Actions_SelectLeft;
        public InputAction @SelectRight => m_Wrapper.m_Actions_SelectRight;
        public InputAction @Scroll => m_Wrapper.m_Actions_Scroll;
        public InputAction @MouseScroll => m_Wrapper.m_Actions_MouseScroll;
        public InputAction @Shop => m_Wrapper.m_Actions_Shop;
        public InputAction @CameraMove => m_Wrapper.m_Actions_CameraMove;
        public InputAction @Move => m_Wrapper.m_Actions_Move;
        public InputAction @Rotate => m_Wrapper.m_Actions_Rotate;
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
                @Refill.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRefill;
                @Refill.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRefill;
                @Refill.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRefill;
                @Fix.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnFix;
                @Fix.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnFix;
                @Fix.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnFix;
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
                @Scroll.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnScroll;
                @MouseScroll.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMouseScroll;
                @MouseScroll.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMouseScroll;
                @MouseScroll.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMouseScroll;
                @Shop.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnShop;
                @Shop.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnShop;
                @Shop.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnShop;
                @CameraMove.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnCameraMove;
                @CameraMove.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnCameraMove;
                @CameraMove.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnCameraMove;
                @Move.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMove;
                @Rotate.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnRotate;
            }
            m_Wrapper.m_ActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Place.started += instance.OnPlace;
                @Place.performed += instance.OnPlace;
                @Place.canceled += instance.OnPlace;
                @Refill.started += instance.OnRefill;
                @Refill.performed += instance.OnRefill;
                @Refill.canceled += instance.OnRefill;
                @Fix.started += instance.OnFix;
                @Fix.performed += instance.OnFix;
                @Fix.canceled += instance.OnFix;
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
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
                @MouseScroll.started += instance.OnMouseScroll;
                @MouseScroll.performed += instance.OnMouseScroll;
                @MouseScroll.canceled += instance.OnMouseScroll;
                @Shop.started += instance.OnShop;
                @Shop.performed += instance.OnShop;
                @Shop.canceled += instance.OnShop;
                @CameraMove.started += instance.OnCameraMove;
                @CameraMove.performed += instance.OnCameraMove;
                @CameraMove.canceled += instance.OnCameraMove;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
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
        void OnRefill(InputAction.CallbackContext context);
        void OnFix(InputAction.CallbackContext context);
        void OnSell(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSwitch(InputAction.CallbackContext context);
        void OnSelectLeft(InputAction.CallbackContext context);
        void OnSelectRight(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
        void OnMouseScroll(InputAction.CallbackContext context);
        void OnShop(InputAction.CallbackContext context);
        void OnCameraMove(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
    }
}
