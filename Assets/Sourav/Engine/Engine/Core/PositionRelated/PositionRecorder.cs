using Sourav.Engine.Core.ApplicationRelated;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.Engine.Core.PositionRelated
{
	public class PositionRecorder : GameElement
	{
		[SerializeField] private string objectName;
		[SerializeField] private Vector3 position;
		[SerializeField] private bool storeLocalPosition;
		
		private void Awake()
		{
			RecordPosition();
		}

		private void Update()
		{
			if (storeLocalPosition)
			{
				if (transform.localPosition != position)
				{
					RecordPosition();
				}
			}
			else
			{
				if (transform.position != position)
				{
					RecordPosition();
				}
			}
		}

		private void RecordPosition()
		{
			NotificationParam recordPosition = new NotificationParam(Mode.stringData | Mode.vector3Data);
			recordPosition.stringData["objectName"] = objectName;
			if (storeLocalPosition)
			{
				var localPosition = transform.localPosition;
				recordPosition.vector3Data["position"] = localPosition;
				position = localPosition;
			}
			else
			{
				var position1 = transform.position;
				recordPosition.vector3Data["position"] = position1;
				position = position1;
			}

			App.Notify(Notification.RecordPositionData, recordPosition);
		}
	}
}
