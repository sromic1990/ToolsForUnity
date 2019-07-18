//Obtained by mixing tutorials and discussions from
//Pan = https://www.youtube.com/watch?v=0G4vcH9N0gc
//Zoom = http://forum.brackeys.com/thread/trying-to-do-camera-pinch-zoom-around-the-center-of-the-pinch/
//Zoom Original = https://answers.unity.com/questions/384753/ortho-camera-zoom-to-mouse-point.html

using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.Utilities.Scripts.Camera.OrthographicPanZoom
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraPanZoom : GameElement 
    {
        [SerializeField] private UnityEngine.Camera camera;
        //Zoom Related
        [SerializeField] private float zoomSpeed = 1f;
        [SerializeField] private float minZoomLevel;
        [SerializeField] private float maxZoomLevel;
        private float previousDistance;
        private Vector3 zoomCenter;
        private bool isZooming = false;
        //Zoom Related
        
        //Pan Related
        private Vector3 _touchStart;
        [SerializeField] private float leftExtent;
        [SerializeField] private float rightExtent;
        [SerializeField] private float topExtent;
        [SerializeField] private float bottomExtent;
        [SerializeField] private Vector2 MinMaxExtentsX;
        [SerializeField] private Vector2 MinMaxExtentsY;
        //Pan Related

        private void Awake()
        {
            camera = GetComponent<UnityEngine.Camera>();
        }
        // Update is called once per frame
        void Update () 
        {
            if(Input.GetMouseButtonDown(0))
            {
                _touchStart = camera.ScreenToWorldPoint(Input.mousePosition);
                _touchStart.z = camera.transform.position.z;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isZooming = false;
            }

            if (Input.touchCount != 2)
            {
                isZooming = false;
            }
            else
            {
                isZooming = true;
            }

            if (isZooming)
            {
                if (Input.touchCount == 2 &&
                    (Input.GetTouch (0).phase == TouchPhase.Began ||
                     Input.GetTouch (1).phase == TouchPhase.Began)) 
                {

                    Vector2 touch1 = Input.GetTouch (0).position;
                    Vector2 touch2 = Input.GetTouch (1).position;
                    //get the initial distance
                    previousDistance = Vector2.Distance (touch1, touch2);
                    zoomCenter = camera.ScreenToWorldPoint ((touch1 + touch2) / 2f);
                    zoomCenter.z = camera.transform.position.z;

                } 
                else if (Input.touchCount == 2 &&
                           (Input.GetTouch (0).phase == TouchPhase.Moved ||
                            Input.GetTouch (1).phase == TouchPhase.Moved)) 
                {

                    float distance;

                    Vector2 touch1 = Input.GetTouch (0).position;
                    Vector2 touch2 = Input.GetTouch (1).position;

                    distance = Vector2.Distance (touch1, touch2);

                    float pinchAmount = (previousDistance - distance) * zoomSpeed * Time.deltaTime;

                    if (camera.orthographic) 
                    {
                        camera.orthographicSize += pinchAmount;
                        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minZoomLevel, maxZoomLevel);
                    } 
                    else 
                    {
                        camera.transform.Translate (0, 0, pinchAmount);
                    }

                    Vector2 midPoint = (touch1 + touch2) / 2f;

                    float multiplier = (1.0f / camera.orthographicSize * pinchAmount);
                    Vector3 zoomTowards = new Vector3(midPoint.x, midPoint.y, camera.transform.position.z);
                    zoomTowards = camera.ScreenToWorldPoint(zoomTowards);
                    // Move camera
                    camera.transform.position -= (zoomTowards - transform.position) * multiplier;

                    previousDistance = distance;
                }
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 direction = _touchStart - camera.ScreenToWorldPoint(Input.mousePosition);
                camera.transform.position += direction;
            }
            
            CheckIfCameraIsWithinBounds();
        }
        
        private void CheckIfCameraIsWithinBounds()
        {
            Vector3 pos = transform.position;

            leftExtent = pos.x - (camera.orthographicSize * camera.aspect);
            rightExtent = pos.x + (camera.orthographicSize * camera.aspect);
            topExtent = pos.y + camera.orthographicSize;
            bottomExtent = pos.y - camera.orthographicSize;

            pos.x = Mathf.Clamp(pos.x, MinMaxExtentsX.x + (camera.orthographicSize * camera.aspect), MinMaxExtentsX.y - (camera.orthographicSize * camera.aspect));
            pos.y = Mathf.Clamp(pos.y, MinMaxExtentsY.x + camera.orthographicSize, MinMaxExtentsY.y - camera.orthographicSize);

            transform.position = pos;
        }
    }
}