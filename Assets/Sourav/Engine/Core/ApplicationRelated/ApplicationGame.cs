using System.Collections.Generic;
using System.Linq;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.ControllerRelated.PauseResumeRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.ControllerRelated;
using Sourav.Engine.Editable.DataRelated;
using UnityEditor;
using UnityEngine;

namespace Sourav.Engine.Core.ApplicationRelated
{
	public class ApplicationGame : MonoBehaviour
	{
		[SerializeField] private NotificationCenter notificationCenter;
		[SerializeField] private List<Controller> controllers;
		[SerializeField] private Transform controllerObject;

		[SerializeField] private Data data;

		#region Mono Methods
		private void Awake()
		{
			if (controllers.Count == 0) //No controllers registered to listen
			{
				controllers = new List<Controller>();
				for (int i = 0; i < controllerObject.childCount; i++)
				{
					Controller controller = controllerObject.GetChild(i).GetComponent<Controller>();
					if (controller != null)
					{
						controllers.Add(controller);
					}
				}
			}
		}
		#endregion
		
		#region Controller Related
		public Controller GetController(ControllerType type)
		{
			Controller c = null;
			for (int i = 0; i < controllers.Count; i++)
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

		public List<Controller> GetAllControllers()
		{
			return controllers;
		}
		#endregion

		#region Pause - Resume Related
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
		#endregion

		#region Notification Related
		public void LockNotification()
		{
			notificationCenter.LockNotification();
		}

		public void UnlockNotification()
		{
			notificationCenter.UnlockNotificationStatus();
		}
		#endregion
		
		#region Data Related
		public Data GetData()
		{
			return data;
		}
		#endregion
	}
}
