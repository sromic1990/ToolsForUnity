using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.Utilities.Scripts.DrawLineRelated
{
    [RequireComponent(typeof(LineRenderer))]
    public class DrawLine : GameElement
    {
        private LineRenderer _line;

        [SerializeField] private GameObject point1;
        [SerializeField] private GameObject point2;

        private bool _canDrawLine = false;

        #region MONO METHODS
        private void Awake()
        {
            _line = GetComponent<LineRenderer>();
        }
        
        private void Update () 
        {
            if (_canDrawLine)
            {
                if (point1 != null && point2 != null)
                {
                    _line.SetPosition(0, point1.transform.position);
                    _line.SetPosition(1, point2.transform.position);
                }
            }	
        }
        #endregion

        #region START STOP INTERNAL
        public void StartDrawingLine(GameObject p1, GameObject p2)
        {
            point1 = p1;
            point2 = p2;
            _canDrawLine = true;
        }
		
        public void StopDrawing()
        {
            _canDrawLine = false;
            _line.SetPosition(0, Vector3.zero);
            _line.SetPosition(1, Vector3.zero);
        }
        #endregion
		
        #region START STOP EXTERNAL
        
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button()]
#else
        [Sourav.Utilities.Scripts.Attributes.Button()]
#endif
        private void StartDrawingExternal()
        {
            if (point1 == null || point2 == null)
            {
                Debug.LogError("Points are null. Please check");
            }
            else
            {
                StartDrawingLine(point1, point2);
            }
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button()]
#else
        [Sourav.Utilities.Scripts.Attributes.Button()]
#endif
        private void StopDrawingExternal()
        {
            StopDrawing();
        }
        #endregion
    }
}