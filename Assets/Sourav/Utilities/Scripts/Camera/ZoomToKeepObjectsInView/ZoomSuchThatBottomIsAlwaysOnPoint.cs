using UnityEngine;

namespace Sourav.Utilities.Scripts.Camera.ZoomToKeepObjectsInView
{
	[RequireComponent(typeof(UnityEngine.Camera))]
	public class ZoomSuchThatBottomIsAlwaysOnPoint : MonoBehaviour
	{
		private UnityEngine.Camera _camera;

		[SerializeField] private float _point;
		
		//Temp Serialize, just to see
		[SerializeField] private Vector2 _extents;
		[SerializeField] private float _deltaY;

		private void Awake()
		{
			_camera = transform.GetComponent<UnityEngine.Camera>();
		}
		
		// Use this for initialization
		void Start () 
		{
		
		}
	
		// Update is called once per frame
		void Update () 
		{
			_extents = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);
		}

		void LateUpdate()
		{
			float deltaYAbs = Mathf.Abs((transform.position.y - _extents.y) - _point);
			_deltaY = (transform.position.y - _extents.y) - _point;
			
			if (deltaYAbs > 0.01f)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y - _deltaY, transform.position.z);
			}
		}
	}
}
