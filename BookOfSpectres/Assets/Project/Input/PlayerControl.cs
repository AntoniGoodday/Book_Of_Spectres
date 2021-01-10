// GENERATED AUTOMATICALLY FROM 'Assets/Project/Input/PlayerControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace PlayerControlNamespace
{
    public class @PlayerControl : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControl()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""Combat"",
            ""id"": ""67d2ffa2-3fd1-47ff-b8f9-077871ea8a7a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""e5e7b155-4cd9-470b-ae16-d19ea8eac8c6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""f304976d-0a1e-4881-84c0-d14a886af3be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=1,behavior=1)""
                },
                {
                    ""name"": ""DialogueSubmit"",
                    ""type"": ""Button"",
                    ""id"": ""a0e658d0-5774-4f3b-9596-4c335a79ca17"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=1,behavior=1)""
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""a1acf3f4-f813-4cf7-897b-3d342fb42a4c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Spell"",
                    ""type"": ""Button"",
                    ""id"": ""3bbdb0e0-f865-46a7-b631-e207453f67bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""38967093-e539-4c86-a5ab-a805da85b5d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""0bf7584f-6f3d-4edc-95b8-819548391034"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d19c53d5-fb7d-4330-87c6-d6e83b2071c7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a4f1f152-87bf-4541-baea-cd8f5f5b3199"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""403e1fea-41ac-4d31-8743-c85ed9cf631e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""771fa71f-1f32-41e2-a6b2-ef8e94062345"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""6f3e0d9b-4c35-4ca1-9519-7f0731530bee"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f13808c9-fe8a-4fe5-a5d0-900442c8fbf2"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""87f445d3-d2a6-4310-b99c-125b7068beda"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ba05813f-a282-47df-81ae-8e195f5062a3"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ec605eb0-8726-464c-a8c8-433111d6fc0e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""451f0f4c-4348-4c27-a2c0-a4dc17745681"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b23fc84c-57f6-4c8b-83f8-5df5f29c9570"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""83795240-98c7-4fea-8d9f-7ba86ed068a9"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2629460a-393c-4ef4-aa24-d97423adb656"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ee9451d8-69f3-4aa2-a291-82f2abe6f69c"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b28423ed-39e6-45a3-b106-78c8f149bb7c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9d37aa3-ec2c-4bee-9352-2287f9924a96"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f25fe62a-ce70-44da-9ad9-0b1925041992"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f1886e7-7fe4-4be6-b80d-0b963b0dda97"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd280dfe-9023-416e-9a21-43bd82b082f7"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3bfdd61-6d43-4013-b751-c2659be1a922"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38b804ad-ddda-4233-93a3-3a2774936d42"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d8b66e1-24af-446d-b787-c703ba21e419"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8837a99-77db-4b30-a0aa-93b1e5482815"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Spell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3ea50f2-4ed7-42b0-b30e-21bc7749b94d"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Spell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41382cee-1aa6-4c7b-98a3-fa132059577b"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Spell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""913e2a7e-d4f8-47aa-941d-bda2717deffe"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f75a1059-692f-483b-b4ba-e17b4af698ae"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88c3277a-e508-4cfb-9ee3-4b48e67d8b69"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""DialogueSubmit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4c0a79e-5b1d-433d-80d5-32590da79d4c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""DialogueSubmit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f631a17b-3d2a-46d7-a1bb-20e2355c9182"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""DialogueSubmit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UIControls"",
            ""id"": ""108520f5-512d-42a4-a6f7-af7cf7dd70a4"",
            ""actions"": [
                {
                    ""name"": ""HorizontalChoice"",
                    ""type"": ""Button"",
                    ""id"": ""6577910d-8499-42ef-a91e-2d85d7a69478"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""ad3569c8-d1d5-4f0f-ae95-dca848a7656c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalChoice"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d835a0d2-66c8-4e61-87d8-b88f5b09dece"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1947006e-51af-4872-bdfd-3bfa33bed4e1"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
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
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
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
            // Combat
            m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
            m_Combat_Move = m_Combat.FindAction("Move", throwIfNotFound: true);
            m_Combat_Submit = m_Combat.FindAction("Submit", throwIfNotFound: true);
            m_Combat_DialogueSubmit = m_Combat.FindAction("DialogueSubmit", throwIfNotFound: true);
            m_Combat_Shoot = m_Combat.FindAction("Shoot", throwIfNotFound: true);
            m_Combat_Spell = m_Combat.FindAction("Spell", throwIfNotFound: true);
            m_Combat_Menu = m_Combat.FindAction("Menu", throwIfNotFound: true);
            // UIControls
            m_UIControls = asset.FindActionMap("UIControls", throwIfNotFound: true);
            m_UIControls_HorizontalChoice = m_UIControls.FindAction("HorizontalChoice", throwIfNotFound: true);
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

        // Combat
        private readonly InputActionMap m_Combat;
        private ICombatActions m_CombatActionsCallbackInterface;
        private readonly InputAction m_Combat_Move;
        private readonly InputAction m_Combat_Submit;
        private readonly InputAction m_Combat_DialogueSubmit;
        private readonly InputAction m_Combat_Shoot;
        private readonly InputAction m_Combat_Spell;
        private readonly InputAction m_Combat_Menu;
        public struct CombatActions
        {
            private @PlayerControl m_Wrapper;
            public CombatActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Combat_Move;
            public InputAction @Submit => m_Wrapper.m_Combat_Submit;
            public InputAction @DialogueSubmit => m_Wrapper.m_Combat_DialogueSubmit;
            public InputAction @Shoot => m_Wrapper.m_Combat_Shoot;
            public InputAction @Spell => m_Wrapper.m_Combat_Spell;
            public InputAction @Menu => m_Wrapper.m_Combat_Menu;
            public InputActionMap Get() { return m_Wrapper.m_Combat; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
            public void SetCallbacks(ICombatActions instance)
            {
                if (m_Wrapper.m_CombatActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnMove;
                    @Submit.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnSubmit;
                    @Submit.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnSubmit;
                    @Submit.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnSubmit;
                    @DialogueSubmit.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnDialogueSubmit;
                    @DialogueSubmit.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnDialogueSubmit;
                    @DialogueSubmit.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnDialogueSubmit;
                    @Shoot.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnShoot;
                    @Shoot.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnShoot;
                    @Shoot.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnShoot;
                    @Spell.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnSpell;
                    @Spell.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnSpell;
                    @Spell.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnSpell;
                    @Menu.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnMenu;
                    @Menu.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnMenu;
                    @Menu.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnMenu;
                }
                m_Wrapper.m_CombatActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Submit.started += instance.OnSubmit;
                    @Submit.performed += instance.OnSubmit;
                    @Submit.canceled += instance.OnSubmit;
                    @DialogueSubmit.started += instance.OnDialogueSubmit;
                    @DialogueSubmit.performed += instance.OnDialogueSubmit;
                    @DialogueSubmit.canceled += instance.OnDialogueSubmit;
                    @Shoot.started += instance.OnShoot;
                    @Shoot.performed += instance.OnShoot;
                    @Shoot.canceled += instance.OnShoot;
                    @Spell.started += instance.OnSpell;
                    @Spell.performed += instance.OnSpell;
                    @Spell.canceled += instance.OnSpell;
                    @Menu.started += instance.OnMenu;
                    @Menu.performed += instance.OnMenu;
                    @Menu.canceled += instance.OnMenu;
                }
            }
        }
        public CombatActions @Combat => new CombatActions(this);

        // UIControls
        private readonly InputActionMap m_UIControls;
        private IUIControlsActions m_UIControlsActionsCallbackInterface;
        private readonly InputAction m_UIControls_HorizontalChoice;
        public struct UIControlsActions
        {
            private @PlayerControl m_Wrapper;
            public UIControlsActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
            public InputAction @HorizontalChoice => m_Wrapper.m_UIControls_HorizontalChoice;
            public InputActionMap Get() { return m_Wrapper.m_UIControls; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIControlsActions set) { return set.Get(); }
            public void SetCallbacks(IUIControlsActions instance)
            {
                if (m_Wrapper.m_UIControlsActionsCallbackInterface != null)
                {
                    @HorizontalChoice.started -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnHorizontalChoice;
                    @HorizontalChoice.performed -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnHorizontalChoice;
                    @HorizontalChoice.canceled -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnHorizontalChoice;
                }
                m_Wrapper.m_UIControlsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @HorizontalChoice.started += instance.OnHorizontalChoice;
                    @HorizontalChoice.performed += instance.OnHorizontalChoice;
                    @HorizontalChoice.canceled += instance.OnHorizontalChoice;
                }
            }
        }
        public UIControlsActions @UIControls => new UIControlsActions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        private int m_ControllerSchemeIndex = -1;
        public InputControlScheme ControllerScheme
        {
            get
            {
                if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
                return asset.controlSchemes[m_ControllerSchemeIndex];
            }
        }
        public interface ICombatActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnSubmit(InputAction.CallbackContext context);
            void OnDialogueSubmit(InputAction.CallbackContext context);
            void OnShoot(InputAction.CallbackContext context);
            void OnSpell(InputAction.CallbackContext context);
            void OnMenu(InputAction.CallbackContext context);
        }
        public interface IUIControlsActions
        {
            void OnHorizontalChoice(InputAction.CallbackContext context);
        }
    }
}
