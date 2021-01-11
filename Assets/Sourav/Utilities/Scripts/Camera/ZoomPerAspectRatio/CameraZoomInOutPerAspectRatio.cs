using UnityEngine;

namespace Sourav.Utilities.Scripts.Camera.ZoomPerAspectRatio
{
	[RequireComponent(typeof(UnityEngine.Camera))]
	public class CameraZoomInOutPerAspectRatio : MonoBehaviour
	{

		private UnityEngine.Camera _camera;
	
		[SerializeField]private float _orthgraphicSize = 5;
		[SerializeField]private float _aspect = 1.33333f;

		private void Awake()
		{
			_camera = GetComponent<UnityEngine.Camera>();
			_orthgraphicSize = _camera.orthographicSize;
			_aspect = _camera.aspect;
		}
	
		void Start () 
		{
			_camera.projectionMatrix = Matrix4x4.Ortho(-_orthgraphicSize * _aspect , _orthgraphicSize * _aspect, -_orthgraphicSize, _orthgraphicSize, _camera.nearClipPlane, _camera.farClipPlane);
		}
	}
}
