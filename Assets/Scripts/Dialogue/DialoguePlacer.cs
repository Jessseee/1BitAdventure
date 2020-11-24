using TMPro;
using UnityEngine;
using Yarn.Unity;

namespace Dialogue
{
    public class DialoguePlacer : MonoBehaviour
    {
        [SerializeField] private DialogueContainer defaultDialogueContainer;

        private TextMeshProUGUI _currentTextElement;
        private DialogueUI _dialogueUI;

        private void Awake()
        {
            _dialogueUI = GetComponent<DialogueUI>();
            _dialogueUI.onLineUpdate.AddListener(UpdateText);
            _dialogueUI.onDialogueEnd.AddListener(DefaultCurrentDialogueContainer);

            if (defaultDialogueContainer != null)
            {
                _dialogueUI.dialogueContainer = defaultDialogueContainer.container;
                _currentTextElement = defaultDialogueContainer.textElement;
            }
            else
                Debug.LogWarning("No default dialogue container set in DialoguePlacer");
            
            // Disable all dialogueContainer is the scene.
            foreach (DialogueContainer dialogueContainer in FindObjectsOfType<DialogueContainer>())
                dialogueContainer.container.SetActive(false);
        }
        
        private void UpdateText(string text)
        {
            _currentTextElement.text = text;
        }

        private void DefaultCurrentDialogueContainer()
        {
            // Disable previously selected dialogueContainer.
            _dialogueUI.dialogueContainer.SetActive(false);
            
            // Set current DialogueContainer to default.
            _dialogueUI.dialogueContainer = defaultDialogueContainer.container;
            _currentTextElement = defaultDialogueContainer.textElement;
        }

        public void SetCurrentDialogueContainer(DialogueContainer dialogueContainer)
        {
            _dialogueUI.dialogueContainer = dialogueContainer.container;
            _currentTextElement = dialogueContainer.textElement;
        }
    }
}
