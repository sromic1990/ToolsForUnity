using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;

namespace Sourav.Engine.Editable.ControllerRelated
{
    public class FirebaseController : Sourav.Engine.Core.ControllerRelated.Controller
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
                        .LogEvent("StageComplete "+App.GetData<LevelCommonData>().CurrentLevel);
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
