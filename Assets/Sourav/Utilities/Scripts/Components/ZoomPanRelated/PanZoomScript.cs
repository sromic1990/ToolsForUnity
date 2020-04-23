using Sirenix.OdinInspector;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.Utilities.Scripts.Components.ZoomPanRelated
{
    public class PanZoomScript : GameElement
    {
        [SerializeField] private float minZoom = 8;  
        [SerializeField] private float maxZoom = 80;
        [SerializeField][ReadOnly] private float defaultZoom = 30; 
        [SerializeField] private float zoomMultiplier = 4;
        [SerializeField] private float spinnerMultiplier = 0.5f;
        [SerializeField] private UnityEngine.Camera zoomCamera;
        [SerializeField] private Spinner spinnerObject;
        
        private Vector3 prevPos = Vector3.zero;
        private Vector3 posDelta = Vector3.zero;

        private bool isFrameSkipped = false;

        private float touchStartX;
        private float touchEndX;

        private bool isInput;
        private bool isMouseUpCalled;

        public void Initialize()
        {
            ZoomValue(defaultZoom);
        }

        public void StartInput()
        {
            isInput = true;
        }
        public void StopInput()
        {
            isInput = false;
        }
        
        private void Update()
        {
            Zoom(Input.GetAxis("Mouse ScrollWheel") * zoomMultiplier);
            
            if (!isInput)
            {
                MouseUp();
                return;
            }

            isMouseUpCalled = false;
            
            if (Input.GetMouseButtonDown(0))
            {
                touchStartX = Input.mousePosition.x;
            }

            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float differnce = currentMagnitude - prevMagnitude;
                Zoom(differnce * 0.01f * zoomMultiplier);
            }
            else if (Input.GetMouseButton(0))
            {
                if (isFrameSkipped)
                {
                    posDelta = Input.mousePosition - prevPos;
                    float angle = Vector3.Dot(posDelta, zoomCamera.transform.right) * spinnerMultiplier;
                    spinnerObject.transform.rotation = Quaternion.Euler(spinnerObject.transform.rotation.eulerAngles.x, spinnerObject.transform.rotation.eulerAngles.y + angle, spinnerObject.transform.rotation.eulerAngles.z);
                }
                else
                {
                    isFrameSkipped = true;
                }
            }

            prevPos = Input.mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                MouseUp();
            }
        }

        private void MouseUp()
        {
            if(isMouseUpCalled)
                return;

            isMouseUpCalled = true;
            
            // D.Log("Mouse up");
            prevPos = Vector3.zero;
            posDelta = Vector3.zero;
            isFrameSkipped = false;

            touchEndX = Input.mousePosition.x;
            // D.Log($"finalPosx = {touchEndX}, startPosx = {touchStartX}");

            if (touchEndX < touchStartX)
            {
                // D.Log($"{touchEndX} < {touchStartX}");
                if (spinnerObject.GetDirection() == SpinningDirection.Clockwise)
                {
                    spinnerObject.ChangeDirection();
                }
            }
            else if (touchEndX > touchStartX)
            {
                // D.Log($"{touchEndX} > {touchStartX}");
                if (spinnerObject.GetDirection() == SpinningDirection.Anticlockwise)
                {
                    spinnerObject.ChangeDirection();
                }
            }
        }

        private void Zoom(float increment)
        {
            zoomCamera.fieldOfView = Mathf.Clamp(zoomCamera.fieldOfView - increment, minZoom, maxZoom);
        }

        private void ZoomValue(float value)
        {
            zoomCamera.fieldOfView = value;
        }
    }
}
