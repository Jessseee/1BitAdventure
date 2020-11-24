using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Dialogue
{
    public class DialogueArrow : MonoBehaviour
    {
        public Sprite[] arrowSprites = new Sprite[2];
        [ReadOnly] public bool flipped;

        private Animator _arrowAnimator;
        private SpriteRenderer _arrowSpriteRenderer;
        private Dictionary<string, string> _animations;

        private void Awake()
        {
            flipped = transform.localScale.x < 0;
            _arrowAnimator = GetComponentInChildren<Animator>();
            _arrowSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            AnimationClip[] animationClips = _arrowAnimator.runtimeAnimatorController.animationClips;
            _animations = new Dictionary<string, string>()
            {
                ["Diagonal"] = animationClips[0].name,
                ["Straight"] = animationClips[1].name,
            };
        }
        
        public void Flip()
        {
            flipped = !flipped;

            Transform newTransform = transform;
            Vector2 theScale = newTransform.localScale;
			
            theScale.x *= -1;
            newTransform.localScale = theScale;
        }

        public void ToggleDiagonal(bool diagonal)
        {
            if (Application.isEditor)
                _arrowSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            else
                _arrowAnimator.Play(diagonal ? _animations["Diagonal"] : _animations["Straight"]);
            
            _arrowSpriteRenderer.sprite = diagonal ? arrowSprites[1] : arrowSprites[0];

                
        }
    }
}