using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.ButtonRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;
using Controller = Sourav.Engine.Core.ControllerRelated.Controller;

namespace Sourav.Engine.Editable.ControllerRelated
{
	public class ButtonController : Core.ControllerRelated.Controller 
	{
		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			
		}

		public void OnButtonPressed(ButtonType button)
		{
			if (App.IsGamePaused())
			{
				Debug.Log("App is paused");
				return;
			}
			switch (button)
			{
				
			}
		}
	}
}
