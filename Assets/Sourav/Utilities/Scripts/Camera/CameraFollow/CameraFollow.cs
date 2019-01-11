using System;
using SwingKing._Scripts.ControllerRelated;
using SwingKing._Scripts.Element;
using UnityEngine;

namespace Sourav.Utilities.Scripts.Camera.CameraFollow
{
	[RequireComponent(typeof(UnityEngine.Camera))]
	public class CameraFollow : Controller
	{
		private UnityEngine.Camera _camera;
		[SerializeField] private Transform _target;
		[Tooltip("Put between 0 and 1")]
		[SerializeField] private float _cameraSmoothSpeed = 0.125f;
		[SerializeField] private Vector3 _offset;

		private bool zoom;
		private bool runScript;

		private void Awake()
		{
			_camera = GetComponent<UnityEngine.Camera>();
		}
		
		private void Update()
		{
			if (zoom)
			{
				_camera.orthographicSize -= App.levelData.CameraZoomInMaxValue * Time.unscaledDeltaTime;
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
				case Notification.LevelReadyToStart:
					_target = App.player.transform;
					runScript = true;
					break;
				
				case Notification.GlassShatterJuice:
					zoom = true;
					break;
				
				case Notification.LevelOver:
					zoom = false;
					break;
				
				case Notification.PlayerHitDeadObstacle:
					runScript = false;
					break;
			}
		}
	}
}
