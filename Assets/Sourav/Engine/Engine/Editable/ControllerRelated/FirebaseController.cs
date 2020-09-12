#if FIREBASE
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;

namespace Sourav.Engine.Editable.ControllerRelated
{
    public class FirebaseController : Controller
    {
        void Start()
        {
            Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        }

        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.LevelComplete:
                    Firebase.Analytics.FirebaseAnalytics
                        .LogEvent("StageComplete "+App.GetLevelData().CurrentLevel);
                    break;
                
                case Notification.AdRewarded:
                    Firebase.Analytics.FirebaseAnalytics
                        .LogEvent("RewardVideoSeen");
                    break;
                
                //case Notification.InterstitialClosed:
                //    Firebase.Analytics.FirebaseAnalytics
                //        .LogEvent("InterstitialShown");
                //    break;
            }
        }
    }
}
#endif
