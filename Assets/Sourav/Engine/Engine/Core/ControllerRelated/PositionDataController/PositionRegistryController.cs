using Sourav.Engine.Core.DataRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using UnityEngine;

namespace Sourav.Engine.Core.ControllerRelated.PositionDataController
{
	public class PositionRegistryController : Controller
	{
		[SerializeField] private LocalPositionRegistry registry;
		
		public override void Init()
		{
			if (registry == null)
			{
				registry = new LocalPositionRegistry();
			}
		}

		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.RecordPositionData:
					registry.StorePosition(param.stringData["objectName"], param.vector3Data["position"]);
					break;
				
				case Notification.FetchPositionData:
					NotificationParam positionData = new NotificationParam(Mode.vector3Data | Mode.stringData);
					positionData.vector3Data["position"] = registry.GetPosition(param.stringData["objectName"]);
					positionData.stringData["objectName"] = param.stringData["objectName"];
					Engine.Core.ApplicationRelated.App.Notify(Notification.PositionData, positionData);
					break;
			}
		}
	}
}
