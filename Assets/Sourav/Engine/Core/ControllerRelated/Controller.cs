using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.ControllerRelated;
using Sourav.Engine.Editable.NotificationRelated;

namespace Sourav.Engine.Core.ControllerRelated
{
	[System.Serializable]
	public abstract class Controller : GameElement
	{
		public ControllerType type;

		public override void Init()
		{
			base.Init();
		}

		public abstract void OnNotificationReceived(Notification notification, NotificationParam param = null);
	}
}
