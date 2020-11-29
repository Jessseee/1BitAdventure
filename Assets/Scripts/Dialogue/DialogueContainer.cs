using TMPro;
using UnityEngine;

namespace Dialogue
{
    public class DialogueContainer : MonoBehaviour
    {
        public GameObject container;
        public TextMeshProUGUI textElement;
        [SerializeField] private DialogueArrow arrow;
        [SerializeField] private GameObject target;
        
        private Canvas _canvas;
        private bool _noTarget;

        private void Awake()
        {
            _noTarget = target == null;
            _canvas = GetComponentInParent<Canvas>();
        }

        private void OnEnable()
        {
            if (_noTarget) return;
            PlaceArrow();
            PlaceContainer();
        }
        
        private void OnDrawGizmos()
        {
            if(target == null) return;
            PlaceArrow();
            PlaceContainer();
        }

        private void PlaceArrow()
        {
            Vector2 targetPosition = target.transform.position;
            float containerRadius = container.GetComponent<RectTransform>().rect.width / 2.0f;
            float minBound = -containerRadius;
            float maxBound = containerRadius;
            
            Transform arrowTransform = arrow.transform;
            arrowTransform.position = new Vector2(targetPosition.x, arrowTransform.position.y);

            if (arrowTransform.localPosition.x >= minBound && arrowTransform.localPosition.x <= maxBound)
            {
                arrowTransform.position = new Vector2(targetPosition.x, arrowTransform.position.y);
                arrow.ToggleDiagonal(false);
            }
            else if (arrowTransform.localPosition.x < minBound)
            {
                arrowTransform.localPosition = new Vector2(minBound, arrowTransform.localPosition.y);
                arrow.ToggleDiagonal(true);
                if (!arrow.flipped)
                    arrow.Flip();
            }
            else if (arrowTransform.localPosition.x > maxBound)
            {
                arrowTransform.localPosition = new Vector2(containerRadius, arrowTransform.localPosition.y);
                arrow.ToggleDiagonal(true);
                if (arrow.flipped)
                    arrow.Flip();
            }
        }

        private void PlaceContainer()
        {
            if (Application.isEditor)
            {
                _canvas = GetComponentInParent<Canvas>();
            }
            
            Vector2 targetPosition = target.transform.position;
            float containerRadius = container.GetComponent<RectTransform>().rect.width / 2.0f + 10;
            float canvasRadius = _canvas.GetComponent<RectTransform>().rect.width / 2.0f - 20;
            float minBound = -canvasRadius + containerRadius;
            float maxBound = canvasRadius - containerRadius;
            
            Transform containerTransform = container.transform;
            containerTransform.position = new Vector2(targetPosition.x, targetPosition.y+1.4f);
            
            if (containerTransform.localPosition.x >= minBound && containerTransform.localPosition.x <= maxBound)
                containerTransform.position = new Vector2(targetPosition.x, containerTransform.position.y);
            else if (containerTransform.localPosition.x < minBound)
                containerTransform.localPosition = new Vector2(minBound, containerTransform.localPosition.y);
            else if (containerTransform.localPosition.x > maxBound)
                containerTransform.localPosition = new Vector2(maxBound, containerTransform.localPosition.y);
        }
    }
}