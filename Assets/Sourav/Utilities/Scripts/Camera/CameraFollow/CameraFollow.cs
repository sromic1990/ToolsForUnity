using System;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.Utilities.Scripts.Camera.CameraFollow
{
	[RequireComponent(typeof(UnityEngine.Camera))]
	public class CameraFollow : Sourav.Engine.Core.ControllerRelated.Controller
	{
		private UnityEngine.Camera _camera;
		[SerializeField] private Transform _target;
		[Tooltip("Put between 0 and 1")]
		[SerializeField][Range(0, 1)] private float _cameraSmoothSpeed = 0.125f;
		[SerializeField] private Vector3 _offset;

		private bool zoom;
		private bool runScript;

		private CameraCommonData cameraCommonData;

		private void Awake()
		{
			_camera = GetComponent<UnityEngine.Camera>();
		}

		private void Start()
		{
			cameraCommonData = App.GetData().GetComponent<CameraCommonData>();
		}

		private void Update()
		{
			if (zoom)
			{
				_camera.orthographicSize -= cameraCommonData.cameraZoomInMaxValue * Time.unscaledDeltaTime;
			}
		}
		
		private void LateUpdate()
		{
			if (!runScript)
				return;
			
			Vector3 desiredPosition = _target.position + _offset;
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.smoothDeltaTime * _cameraSmoothSpeed);
			transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
		}

		public override void Init()
		{
			runScript = false;
			zoom = false;
		}

		public override void OnNotificationReceived(Notification notification, NotificationParam optionalObject = null)
		{
			switch (notification)
			{
				case Notification.StartCameraScript:
					_target = App.GetData().GetComponent<GameCommonData>().player;
					runScript = true;
					break;
				
				case Notification.StartZoomingCamera:
					zoom = true;
					break;
				
				case Notification.StopZoomingCamera:
					zoom = false;
					break;
				
				case Notification.StopCameraScript:
					runScript = false;
					break;
			}
		}
	}
}
