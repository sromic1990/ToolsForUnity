using System.Collections.Generic;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.Engine.Core.NotificationRelated
{
	[System.Serializable]
	public class NotificationCenter : GameElement
	{
		private List<Core.ControllerRelated.Controller> controllers;

		private Queue<NotificationQueue> notificationQueue = new Queue<NotificationQueue>();

		[SerializeField] private NotificationStatus status;
		private bool isNotificationsQueued
		{
			get
			{
				return notificationQueue.Count > 0 ? true : false;
			}
		}

		public void Awake()
		{
			UnlockNotificationStatus();
			notificationQueue.Clear();

			if (controllers == null)
			{
				controllers = App.GetAllControllers();
			}
		}
		
		public void Notify(Notification notification, NotificationParam param = null)
		{
			switch (status)
			{
				case NotificationStatus.Locked:
					if (param != null)
					{
						if (param.shouldQueue)
						{
							NotificationQueue nq = new NotificationQueue(notification, param);
							notificationQueue.Enqueue(nq);
						}
					}
					break;
				
				case NotificationStatus.Unlocked:
					NotifyAllControllers(notification, param);
					break;
			}
		}

		public void LockNotification()
		{
			status = NotificationStatus.Locked;
		}

		public void UnlockNotificationStatus()
		{
			status = NotificationStatus.Unlocked;
			if (isNotificationsQueued)
			{
				for (int i = 0; i < notificationQueue.Count; i++)
				{
					NotificationQueue nq = notificationQueue.Dequeue();
					NotifyAllControllers(nq.Notification, nq.param);
				}
			}
		}

		private void NotifyAllControllers(Notification notification, NotificationParam param)
		{
			if (controllers == null)
				return;
			// D.Log("Notification = "+notification);
			for (int i = 0; i < controllers.Count; i++)
			{
				controllers[i].OnNotificationReceived(notification, param);
			}
		}

		private struct NotificationQueue
		{
			public Notification Notification;
			public NotificationParam param;

			public NotificationQueue(Notification notification, NotificationParam param)
			{
				this.Notification = notification;
				this.param = param;
			}
		}

		private enum NotificationStatus
		{
			Unlocked,
			Locked
		}

	}
}
