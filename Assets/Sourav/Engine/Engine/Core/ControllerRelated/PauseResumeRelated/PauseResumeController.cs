﻿using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using UnityEngine;

namespace Sourav.Engine.Core.ControllerRelated.PauseResumeRelated
{
	public class PauseResumeController : Controller
	{
		private bool isPaused;
		public bool IsPaused
		{
			get { return isPaused; }
		}

		private void Pause()
		{
			isPaused = true;
			Engine.Core.ApplicationRelated.App.Notify(Notification.GamePaused);
		}

		private void Resume()
		{
			isPaused = false;
			Engine.Core.ApplicationRelated.App.Notify(Notification.GameResumed);
		}

		public override void Init()
		{
			Resume();
		}

		private void TogglePauseStatus()
		{
			if (IsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}

		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.PauseGame:
					Pause();
					break;
				
				case Notification.ResumeGame:
					Resume();
					break;
				
				case Notification.PauseResumeToggle:
					TogglePauseStatus();
					break;
			}
		}
	}
}
