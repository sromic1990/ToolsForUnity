using Sourav.Engine.Core.DataRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
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
					registry.StorePosition(param.stringData[0], param.vector3Data[0]);
					break;
				
				case Notification.FetchPositionData:
					NotificationParam positionData = new NotificationParam(Mode.vector3Data | Mode.stringData);
					positionData.vector3Data.Add(registry.GetPosition(param.stringData[0]));
					positionData.stringData.Add(param.stringData[0]);
					App.GetNotificationCenter().Notify(Notification.PositionData, positionData);
					break;
			}
		}
	}
}
