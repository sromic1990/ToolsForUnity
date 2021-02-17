#if NV
using MoreMountains.NiceVibrations;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.Engine.Core.ControllerRelated
{
	public class HapticFeedbackControllerNV : Controller
	{
		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			if (!AppGame.GetLevelData().IsVibrationOn)
			{
				return;
			}
			
			switch (notification)
			{
				case Notification.HapticSelectionChange:
					PlayHaptic(HapticTypes.Selection);
					break;
				
				case Notification.HapticImpactLight:
					PlayHaptic(HapticTypes.LightImpact);
					break;
				
				case Notification.HapticImpactMedium:
					PlayHaptic(HapticTypes.MediumImpact);
					break;
				
				case Notification.HapticImpactHeavy:
					PlayHaptic(HapticTypes.HeavyImpact);
					break;
				
				case Notification.HapticSuccess:
					PlayHaptic(HapticTypes.Success);
					break;
				
				case Notification.HapticWarning:
					PlayHaptic(HapticTypes.Warning);
					break;
				
				case Notification.HapticFailure:
					PlayHaptic(HapticTypes.Failure);
					break;
				
				case Notification.HapticSoft:
					PlayHaptic(HapticTypes.SoftImpact);
					break;
				
				case Notification.HapticRigid:
					PlayHaptic(HapticTypes.RigidImpact);
					break;
			}
		}

		private void AddToAndroidManifest()
		{
			Handheld.Vibrate();
		}

		private void PlayHaptic(HapticTypes type)
		{
			#if !UNITY_EDITOR
			MMVibrationManager.Haptic (type);
			#endif
		}
	}
}
#endif