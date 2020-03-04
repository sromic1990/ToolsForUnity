using System.Collections.Generic;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.ControllerRelated.PauseResumeRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.AdditionalLogicRelated;
using Sourav.Engine.Editable.ControllerRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Editable.Timer;
using Sourav.IdleGameEngine.IdleGameData;
using Sourav.IdleGameEngine.UpdateRelated;
using Sourav.Utilities.Scripts.Timer;
using UnityEngine;

namespace Sourav.Engine.Core.ApplicationRelated
{
	public class ApplicationGame : MonoBehaviour
	{
		[SerializeField] private NotificationCenter notificationCenter;
		[SerializeField] private List<Core.ControllerRelated.Controller> controllers;
		[SerializeField] private Transform controllerObject;
		[SerializeField] private CoroutineHandler coroutineHandler;
		[SerializeField] private UpdateElement updateElement;

		[SerializeField] private CommonData commonData;

		[SerializeField] private LevelCommonData levelCommonData;
		[SerializeField] private TimerData timerData;

		#if IDLEGAME
		[SerializeField] private IdleCommonData idleCommonData;
		#endif

		[SerializeField] private LogicEvaluator logicEvaluator;

		#region Controller Related
		public Core.ControllerRelated.Controller GetController(ControllerType type)
		{
			Core.ControllerRelated.Controller c = null;
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

		public List<Core.ControllerRelated.Controller> GetAllControllers()
		{
			return controllers;
		}

		public void SetControllers(Core.ControllerRelated.Controller[] controllers)
		{
			this.controllers = new List<Core.ControllerRelated.Controller>();
			for (int i = 0; i < controllers.Length; i++)
			{
				this.controllers.Add(controllers[i]);
			}
		}

		public CoroutineHandler GetCoroutineHandler()
		{
			return coroutineHandler;
		}

		public void Notify(Notification notification, NotificationParam param = null)
		{
			notificationCenter.Notify(notification, param);
		}

		public UpdateElement GetUpdater()
		{
			return updateElement;
		}
		#endregion

		#region Pause - Resume Related
		public bool IsGamePaused()
		{
			bool isPaused = false;
			Core.ControllerRelated.Controller pauseController = GetController(ControllerType.PauseResumeController);
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
		public CommonData GetData()
		{
			return commonData;
		}

		public LevelCommonData GetLevelData()
		{
			return levelCommonData;
		}

		public TimerData GetTimerData()
		{
			return timerData;
		}

		#if IDLEGAME
		public IdleCommonData GetIdleData()
		{
			return idleCommonData;
		}
		#endif
		#endregion
		
		#region Element Related
		public LogicEvaluator GetLogicEvaluator()
		{
			return logicEvaluator;
		}
		#endregion
	}
}
