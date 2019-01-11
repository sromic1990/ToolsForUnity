using System.Collections.Generic;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.ControllerRelated;
using Sourav.Engine.Editable.NotificationRelated;

namespace Sourav.Engine.Core.NotificationRelated
{
	[System.Serializable]
	public class NotificationCenter : GameElement
	{
		private List<Controller> controllers;

		private Queue<NotificationQueue> notificationQueue = new Queue<NotificationQueue>();

		private NotificationStatus status;
		private bool isNotificationsQueued
		{
			get
			{
				return notificationQueue.Count > 0 ? true : false;
			}
		}

		public override void Init()
		{
			UnlockNotificationStatus();
			notificationQueue.Clear();

			if (controllers == null)
			{
				controllers = App.GetAllControllers();
			}
		}
		
		public void Notify(ControllerType notifyingController, bool canListenToOwnNotification, Notification notification, NotificationParam param = null)
		{
			switch (status)
			{
				case NotificationStatus.Locked:
					if (param != null)
					{
						if (param.shouldQueue)
						{
							NotificationQueue nq = new NotificationQueue(notifyingController, canListenToOwnNotification, notification, param);
							notificationQueue.Enqueue(nq);
						}
					}
					break;
				
				case NotificationStatus.Unlocked:
					NotifyAllControllers(notifyingController, canListenToOwnNotification, notification, param);
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
					NotifyAllControllers(nq.notifyingController, nq.canListenToOwnNotification, nq.Notification, nq.param);
				}
			}
		}

		private void NotifyAllControllers(ControllerType notifyingController, bool canListenToOwnNotification, Notification notification, NotificationParam param)
		{
			if (controllers == null)
				return;
			
			for (int i = 0; i < controllers.Count; i++)
			{
				if(controllers[i].type == notifyingController && !canListenToOwnNotification) continue;
				controllers[i].OnNotificationReceived(notification, param);
			}
		}

		private struct NotificationQueue
		{
			public ControllerType notifyingController;
			public bool canListenToOwnNotification;
			public Notification Notification;
			public NotificationParam param;

			public NotificationQueue(ControllerType notifyingController, bool canListenToOwnNotification, Notification notification, NotificationParam param)
			{
				this.notifyingController = notifyingController;
				this.canListenToOwnNotification = canListenToOwnNotification;
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
