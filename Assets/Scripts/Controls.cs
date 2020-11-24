// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""player"",
            ""id"": ""145f80c1-4900-42ec-a9d8-4630dfcd21e0"",
            ""actions"": [
                {
                    ""name"": ""move"",
                    ""type"": ""Button"",
                    ""id"": ""03faf8bb-31e6-4293-b9eb-762a73e9d740"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""jump"",
                    ""type"": ""Button"",
                    ""id"": ""c7a3507b-1d31-4466-9b61-e9beea5dccda"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""4ede87dd-753a-4322-86dd-0d93d9364e33"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""11c5d534-442b-497e-9e6a-2f9bcf1cb8cf"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""65b69293-fce8-4c84-90f8-957bad91f256"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""05954431-eedc-4f8d-8d16-b4806f5613a2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ac7f98dc-4822-406a-8b56-a1a94ab431bb"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""00daf1df-e448-42e3-9869-eecd4642e27d"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d2a1608f-2268-4f03-9e70-efa4df1b7fc3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b713c0e-434c-4e89-994f-1c77885fdd3a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1f7e9cd-c6b0-41b0-92dd-360f242f2ec9"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""dialogue"",
            ""id"": ""71ef87a5-e6b9-41ca-ae06-9776f381b839"",
            ""actions"": [
                {
                    ""name"": ""continue"",
                    ""type"": ""Button"",
                    ""id"": ""fa3bb3c3-9e23-4dc0-a26a-f686c338b8fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""639fdce0-25ac-4a30-8851-bdc40f29f150"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""game"",
            ""id"": ""86ec9a30-cee1-4396-99e3-391cfec1829f"",
            ""actions"": [
                {
                    ""name"": ""exit"",
                    ""type"": ""Button"",
                    ""id"": ""89b7c749-53c7-4c68-bfee-cc784a80ceeb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0a8dae3f-5d90-4fcf-a62c-fe99634bdafe"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // player
        m_player = asset.FindActionMap("player", throwIfNotFound: true);
        m_player_move = m_player.FindAction("move", throwIfNotFound: true);
        m_player_jump = m_player.FindAction("jump", throwIfNotFound: true);
        // dialogue
        m_dialogue = asset.FindActionMap("dialogue", throwIfNotFound: true);
        m_dialogue_continue = m_dialogue.FindAction("continue", throwIfNotFound: true);
        // game
        m_game = asset.FindActionMap("game", throwIfNotFound: true);
        m_game_exit = m_game.FindAction("exit", throwIfNotFound: true);
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

    // player
    private readonly InputActionMap m_player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_player_move;
    private readonly InputAction m_player_jump;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @move => m_Wrapper.m_player_move;
        public InputAction @jump => m_Wrapper.m_player_jump;
        public InputActionMap Get() { return m_Wrapper.m_player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @move.started += instance.OnMove;
                @move.performed += instance.OnMove;
                @move.canceled += instance.OnMove;
                @jump.started += instance.OnJump;
                @jump.performed += instance.OnJump;
                @jump.canceled += instance.OnJump;
            }
        }
    }
    public PlayerActions @player => new PlayerActions(this);

    // dialogue
    private readonly InputActionMap m_dialogue;
    private IDialogueActions m_DialogueActionsCallbackInterface;
    private readonly InputAction m_dialogue_continue;
    public struct DialogueActions
    {
        private @Controls m_Wrapper;
        public DialogueActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @continue => m_Wrapper.m_dialogue_continue;
        public InputActionMap Get() { return m_Wrapper.m_dialogue; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogueActions set) { return set.Get(); }
        public void SetCallbacks(IDialogueActions instance)
        {
            if (m_Wrapper.m_DialogueActionsCallbackInterface != null)
            {
                @continue.started -= m_Wrapper.m_DialogueActionsCallbackInterface.OnContinue;
                @continue.performed -= m_Wrapper.m_DialogueActionsCallbackInterface.OnContinue;
                @continue.canceled -= m_Wrapper.m_DialogueActionsCallbackInterface.OnContinue;
            }
            m_Wrapper.m_DialogueActionsCallbackInterface = instance;
            if (instance != null)
            {
                @continue.started += instance.OnContinue;
                @continue.performed += instance.OnContinue;
                @continue.canceled += instance.OnContinue;
            }
        }
    }
    public DialogueActions @dialogue => new DialogueActions(this);

    // game
    private readonly InputActionMap m_game;
    private IGameActions m_GameActionsCallbackInterface;
    private readonly InputAction m_game_exit;
    public struct GameActions
    {
        private @Controls m_Wrapper;
        public GameActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @exit => m_Wrapper.m_game_exit;
        public InputActionMap Get() { return m_Wrapper.m_game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @exit.started -= m_Wrapper.m_GameActionsCallbackInterface.OnExit;
                @exit.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnExit;
                @exit.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnExit;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @exit.started += instance.OnExit;
                @exit.performed += instance.OnExit;
                @exit.canceled += instance.OnExit;
            }
        }
    }
    public GameActions @game => new GameActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IDialogueActions
    {
        void OnContinue(InputAction.CallbackContext context);
    }
    public interface IGameActions
    {
        void OnExit(InputAction.CallbackContext context);
    }
}
