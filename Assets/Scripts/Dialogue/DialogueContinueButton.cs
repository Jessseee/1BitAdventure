using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace Dialogue
{
    public class DialogueContinueButton : MonoBehaviour
    {
        private Image _background;
        private Animator _animator;
        private Dictionary<string, string> _animations;
        private DialogueUI _dialogueUI;

        private void Awake()
        {
            _dialogueUI = FindObjectOfType<DialogueUI>();
            
            _background = GetComponentInParent<Image>();
            _background.color = Color.clear;

            _animator = GetComponent<Animator>();
            AnimationClip[] animationClips = _animator.runtimeAnimatorController.animationClips;
            _animations = new Dictionary<string, string>()
            {
                ["FadeIn"] = animationClips[0].name,
                ["FadeOut"] = animationClips[1].name,
            };

            _dialogueUI.onLineFinishDisplaying.AddListener(FadeIn);
            _dialogueUI.onLineStart.AddListener(FadeOut);
            _dialogueUI.onDialogueEnd.AddListener(FadeOut);
        }

        private void FadeIn()
        {
            _background.color = Color.black;
            _animator.Play(_animations["FadeIn"]);
        }

        private void FadeOut()
        {
            _background.color = Color.clear;
            _animator.Play(_animations["FadeOut"]);
        }
    }
}
