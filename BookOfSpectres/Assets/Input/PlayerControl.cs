// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerControl.inputactions'

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
            ""name"": ""DefaultControls"",
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
                    ""interactions"": """"
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
                    ""interactions"": ""Press(behavior=1)"",
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
                    ""interactions"": """",
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
            // DefaultControls
            m_DefaultControls = asset.FindActionMap("DefaultControls", throwIfNotFound: true);
            m_DefaultControls_Move = m_DefaultControls.FindAction("Move", throwIfNotFound: true);
            m_DefaultControls_Submit = m_DefaultControls.FindAction("Submit", throwIfNotFound: true);
            m_DefaultControls_Shoot = m_DefaultControls.FindAction("Shoot", throwIfNotFound: true);
            m_DefaultControls_Spell = m_DefaultControls.FindAction("Spell", throwIfNotFound: true);
            m_DefaultControls_Menu = m_DefaultControls.FindAction("Menu", throwIfNotFound: true);
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

        // DefaultControls
        private readonly InputActionMap m_DefaultControls;
        private IDefaultControlsActions m_DefaultControlsActionsCallbackInterface;
        private readonly InputAction m_DefaultControls_Move;
        private readonly InputAction m_DefaultControls_Submit;
        private readonly InputAction m_DefaultControls_Shoot;
        private readonly InputAction m_DefaultControls_Spell;
        private readonly InputAction m_DefaultControls_Menu;
        public struct DefaultControlsActions
        {
            private @PlayerControl m_Wrapper;
            public DefaultControlsActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_DefaultControls_Move;
            public InputAction @Submit => m_Wrapper.m_DefaultControls_Submit;
            public InputAction @Shoot => m_Wrapper.m_DefaultControls_Shoot;
            public InputAction @Spell => m_Wrapper.m_DefaultControls_Spell;
            public InputAction @Menu => m_Wrapper.m_DefaultControls_Menu;
            public InputActionMap Get() { return m_Wrapper.m_DefaultControls; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(DefaultControlsActions set) { return set.Get(); }
            public void SetCallbacks(IDefaultControlsActions instance)
            {
                if (m_Wrapper.m_DefaultControlsActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnMove;
                    @Submit.started -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnSubmit;
                    @Submit.performed -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnSubmit;
                    @Submit.canceled -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnSubmit;
                    @Shoot.started -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnShoot;
                    @Shoot.performed -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnShoot;
                    @Shoot.canceled -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnShoot;
                    @Spell.started -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnSpell;
                    @Spell.performed -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnSpell;
                    @Spell.canceled -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnSpell;
                    @Menu.started -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnMenu;
                    @Menu.performed -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnMenu;
                    @Menu.canceled -= m_Wrapper.m_DefaultControlsActionsCallbackInterface.OnMenu;
                }
                m_Wrapper.m_DefaultControlsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Submit.started += instance.OnSubmit;
                    @Submit.performed += instance.OnSubmit;
                    @Submit.canceled += instance.OnSubmit;
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
        public DefaultControlsActions @DefaultControls => new DefaultControlsActions(this);
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
        public interface IDefaultControlsActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnSubmit(InputAction.CallbackContext context);
            void OnShoot(InputAction.CallbackContext context);
            void OnSpell(InputAction.CallbackContext context);
            void OnMenu(InputAction.CallbackContext context);
        }
    }
}
