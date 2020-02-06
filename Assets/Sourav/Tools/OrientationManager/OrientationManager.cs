using Sourav.Engine.Core.DebugRelated;
using UnityEngine;
using UnityEngine.Events;

namespace Sourav.Tools.OrientationManager
{
    [RequireComponent(typeof(RectTransform), typeof(Canvas))]
    [DisallowMultipleComponent]
    public class OrientationManager : MonoBehaviour
    {
        protected OrientationManager() { }

        private RectTransform m_rectTransform;
        public RectTransform RectTransform { get { if (m_rectTransform == null) { m_rectTransform = GetComponent<RectTransform>(); } return m_rectTransform; } }

        private Canvas m_canvas;
        public Canvas Canvas { get { if (m_canvas == null) { m_canvas = GetComponent<Canvas>(); } return m_canvas; } }

        /// <summary>
        /// Prints to Debug.Log all the relevant functionality informations needed for debug purposes
        /// </summary>
        public bool debug = false;

        /// <summary>
        /// Orientation type
        /// </summary>
        public enum Orientation
        {
            /// <summary>
            /// Landscape mode
            /// </summary>
            Landscape,
            /// <summary>
            /// Portrait mode
            /// </summary>
            Portrait,
            /// <summary>
            /// Unknown mode. Used for calibration purposes
            /// </summary>
            Unknown
        }

        [System.Serializable]
        public class OrientationChange : UnityEvent<Orientation> { }
        /// <summary>
        /// UnityEvent that sends an OrientaionManager.Orientation parameter when the device's orientation changes.
        /// </summary>
        public OrientationChange onOrientationChange = new OrientationChange();

        private Orientation currentOrientation = Orientation.Unknown;
        /// <summary>
        /// Retruns the current orientation of the device.
        /// </summary>
        public Orientation CurrentOrientation { get { return currentOrientation; } }

        private void OnEnable()
        {
            CheckDeviceOrientation();
        }

        void OnRectTransformDimensionsChange()
        {
            CheckDeviceOrientation();
        }

        /// <summary>
        /// Checks the current orientation and updates it if it changed since the last check. You do not need to call this yourself as this is called automatically by the OrientationManager in the most efficient way. 
        /// </summary>
        public void CheckDeviceOrientation()
        {
#if UNITY_EDITOR
            //PORTRAIT
            if (Screen.width < Screen.height)
            {
                if (currentOrientation != Orientation.Portrait) //Orientation changed to PORTRAIT
                {
                    ChangeOrientation(Orientation.Portrait);
                }
            }

            //LANDSCAPE
            else
            {
                if (currentOrientation != Orientation.Landscape) //Orientation changed to LANDSCAPE
                {
                    ChangeOrientation(Orientation.Landscape);
                }
            }
#else
            //LANDSCAPE
            if (Screen.orientation == ScreenOrientation.Landscape ||
               Screen.orientation == ScreenOrientation.LandscapeLeft ||
               Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                if (currentOrientation != Orientation.Landscape) //Orientation changed to LANDSCAPE
                {
                    ChangeOrientation(Orientation.Landscape);
                }
            }

            //PORTRAIT
            else if (Screen.orientation == ScreenOrientation.Portrait ||
                     Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                if (currentOrientation != Orientation.Portrait) //Orientation changed to PORTRAIT
                {
                    ChangeOrientation(Orientation.Portrait);
                }
            }

            //FALLBACK option if we are in AutoRotate or if we are in Unknown
            else
            {
                ChangeOrientation(Orientation.Landscape);
            }
#endif
        }

        /// <summary>
        /// Updates the currentOrientation to the specified value and sends an UnityEvent to signal the change.
        /// </summary>
        public void ChangeOrientation(Orientation newOrientation)
        {
            currentOrientation = newOrientation;
            onOrientationChange.Invoke(currentOrientation);
            if (debug) D.Log("[OrientationManager] currentOrientation: " + currentOrientation.ToString());
        }
    }
}
