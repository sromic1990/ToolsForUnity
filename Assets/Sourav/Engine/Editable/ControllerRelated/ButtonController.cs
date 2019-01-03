using Sourav.Engine.Core;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.ButtonRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.ControllerRelated
{
	public class ButtonController : Controller 
	{
		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			
		}

		public void OnButtonPressed(ButtonType button)
		{
			Debug.Log(string.Format("button Pressed = {0}", button.ToString()));
		}
	}
}
