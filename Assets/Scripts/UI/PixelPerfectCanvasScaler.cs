using System;

/* Sourced from Unity's 2d pixel perfect example repository
 * https://github.com/Unity-Technologies/2d-pixel-perfect/tree/master/Assets/Unity%20Technologies/2D%20Pixel%20Perfect/Xtras/Custom%20Canvas%20Scaler
 */

namespace UnityEngine.UI
{
    public class PixelPerfectCanvasScaler : CanvasScaler
    {
        private Canvas _mRootCanvas;
        private const float KLogBase = 2;

        protected override void Awake()
        {
            _mRootCanvas = GetComponent<Canvas>();
        }

        protected override void HandleScaleWithScreenSize()
        {
            Vector2 screenSize;
            if (_mRootCanvas.worldCamera != null)
            {
                Camera worldCamera = _mRootCanvas.worldCamera;
                screenSize = new Vector2(worldCamera.pixelWidth, worldCamera.pixelHeight);
            }
            else
            {
                screenSize = new Vector2(Screen.width, Screen.height);
            }

            // Multiple display support only when not the main display. For display 0 the reported
            // resolution is always the desktops resolution since its part of the display API,
            // so we use the standard none multiple display method. (case 741751)
            int displayIndex = _mRootCanvas.targetDisplay;
            if (displayIndex > 0 && displayIndex < Display.displays.Length)
            {
                Display display = Display.displays[displayIndex];
                screenSize = new Vector2(display.renderingWidth, display.renderingHeight);
            }

            float scalingFactor = 0;
            switch (m_ScreenMatchMode)
            {
                case ScreenMatchMode.MatchWidthOrHeight:
                {
                    // We take the log of the relative width and height before taking the average.
                    // Then we transform it back in the original space.
                    // the reason to transform in and out of logarithmic space is to have better behavior.
                    // If one axis has twice resolution and the other has half, it should even out if widthOrHeight value is at 0.5.
                    // In normal space the average would be (0.5 + 2) / 2 = 1.25
                    // In logarithmic space the average is (-1 + 1) / 2 = 0
                    float logWidth = Mathf.Log(screenSize.x / m_ReferenceResolution.x, KLogBase);
                    float logHeight = Mathf.Log(screenSize.y / m_ReferenceResolution.y, KLogBase);
                    float logWeightedAverage = Mathf.Lerp(logWidth, logHeight, m_MatchWidthOrHeight);
                    scalingFactor = Mathf.Pow(KLogBase, logWeightedAverage);
                    break;
                }
                case ScreenMatchMode.Expand:
                {
                    
                    scalingFactor = Mathf.Min(screenSize.x / m_ReferenceResolution.x, screenSize.y / m_ReferenceResolution.y);
                    break;
                }
                case ScreenMatchMode.Shrink:
                {
                    scalingFactor = Mathf.Max(screenSize.x / m_ReferenceResolution.x, screenSize.y / m_ReferenceResolution.y);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SetScaleFactor(scalingFactor);
            SetReferencePixelsPerUnit(m_ReferencePixelsPerUnit);
        }
    }
}
