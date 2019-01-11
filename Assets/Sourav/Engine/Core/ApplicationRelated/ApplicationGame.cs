using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.ControllerRelated.PauseResumeRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.ControllerRelated;
using Sourav.Engine.Editable.DataRelated;
using UnityEditor;
using UnityEngine;

namespace Sourav.Engine.Core.ApplicationRelated
{
	public abstract class ApplicationGame : MonoBehaviour
	{
		[SerializeField] private NotificationCenter notificationCenter;
		[SerializeField] private Controller[] controllers;
		[SerializeField] private GameObject controllerObject;

		[SerializeField] private LevelData levelData;
		[SerializeField] private CameraData cameraData;

		public Transform player;

		private void Awake()
		{
			controllers = controllerObject.GetComponents<Controller>();
		}
		
		public Controller GetController(ControllerType type)
		{
			Controller c = null;
			for (int i = 0; i < controllers.Length; i++)
			{
				if (controllers[i].type == type)
				{
					c = controllers[i];
					break;
				}
			}

			return c;
		}

		public NotificationCenter GetNotificationCenter()
		{
			return notificationCenter;
		}

		public Controller[] GetAllControllers()
		{
			return controllers;
		}

		public bool IsGamePaused()
		{
			bool isPaused = false;
			Controller pauseController = GetController(ControllerType.PauseResumeController);
			if (pauseController != null)
			{
				if (pauseController is PauseResumeController)
				{
					isPaused = ((PauseResumeController) pauseController).IsPaused;
				}
			}

			return isPaused;
		}

		public void LockNotification()
		{
			notificationCenter.LockNotification();
		}

		public void UnlockNotification()
		{
			notificationCenter.UnlockNotificationStatus();
		}
		
		//Data Related
		public LevelData GetLevelData()
		{
			return levelData;
		}
		public CameraData GetCameraData()
		{
			return cameraData;	
		}
	}
}
