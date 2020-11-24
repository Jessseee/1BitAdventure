using UnityEngine;
using Yarn.Unity;

namespace Player
{
    [RequireComponent(typeof(PlayerController))]
    public class InputHandler : MonoBehaviour
    {
        private PlayerController _player;
        private Controls _controls;
        private float _walkDirection;
        private DialogueUI _dialogueUI;
        private DialogueRunner _dialogueRunner;

        private void Awake()
        {
            _dialogueUI = FindObjectOfType<DialogueUI>();
            _dialogueRunner = FindObjectOfType<DialogueRunner>();
            _player = GetComponent<PlayerController>();
            _controls = new Controls();

            _controls.player.jump.performed += ctx => _player.Jump();
            _controls.dialogue.@continue.performed += ctx => _dialogueUI.MarkLineComplete();
            _controls.game.exit.performed += ctx => _dialogueRunner.StartDialogue("Quit");
            
            _dialogueRunner.AddCommandHandler(
                "Quit",
                Quit
            );
        }
        
        private static void Quit(string[] parameters)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        private void OnEnable()
        {
            _controls.player.Enable();
            _controls.dialogue.Enable();
            _controls.game.Enable();
        }

        private void OnDisable()
        {
            _controls.player.Disable();
            _controls.dialogue.Disable();
            _controls.game.Enable();
        }

        private void FixedUpdate()
        {
            _walkDirection = _controls.player.move.ReadValue<float>();
            _player.Move(_walkDirection * Time.fixedDeltaTime);
        }
        
        public void TogglePlayerInput(bool enable)
        {
            if(enable)
                _controls.player.Enable();
            else
                _controls.player.Disable();
        }
    }
}
