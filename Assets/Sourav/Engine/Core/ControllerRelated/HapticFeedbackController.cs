using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.Engine.Core.ControllerRelated
{
	public class HapticFeedbackController : Controller
	{
		[SerializeField] private iOSHapticFeedback.iOSFeedbackType feedbackTypeSuccess;
		[SerializeField] private iOSHapticFeedback.iOSFeedbackType feedbackTypeFailure;
		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			if (!App.GetLevelData().IsVibrationOn)
			{
				return;
			}
			
			switch (notification)
			{
				case Notification.HapticSuccess:
					HapticForSubmitButtonSuccess();
					break;
				
				case Notification.HapticFailure:
					HapticForSubmitButtonFailure();
					break;
			}
		}

		private void HapticForSubmitButtonFailure()
		{
#if UNITY_IOS
			if (iOSHapticFeedback.Instance.IsSupported())
			{
				iOSHapticFeedback.Instance.Trigger(feedbackTypeFailure);
			}
#endif
		}

		private void HapticForSubmitButtonSuccess()
		{
			#if UNITY_IOS
			if (iOSHapticFeedback.Instance.IsSupported())
			{
				iOSHapticFeedback.Instance.Trigger(feedbackTypeSuccess);
			}
			#endif
		}
	}
}
