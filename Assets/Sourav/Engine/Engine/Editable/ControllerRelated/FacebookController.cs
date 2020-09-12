#if FACEBOOK
using System.Collections.Generic;
using Facebook.Unity;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;

namespace Sourav.Engine.Editable.ControllerRelated
{
    public class FacebookController : Controller 
    {
        void Awake ()
        {
            if (FB.IsInitialized) {
                FB.ActivateApp();
            } else {
                //Handle FB.Init
                FB.Init( () => {
                    FB.ActivateApp();
                });
            }
        }
        
        void OnApplicationPause (bool pauseStatus)
        {
            // Check the pauseStatus to see if we are in the foreground
            // or background
            if (!pauseStatus) {
                //app resume
                if (FB.IsInitialized) {
                    FB.ActivateApp();
                } else {
                    //Handle FB.Init
                    FB.Init( () => {
                        FB.ActivateApp();
                    });
                }
            }
        }
        
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.LevelComplete:
                    FbLogLevel();
                    break;
                
                case Notification.AdRewarded:
                    FbLogCompletedRewardVideo();
                    break;
            }
        }

        private void FbLogLevel()
        {
            string currentLevel = "Level Complete";
            var levelParams = new Dictionary<string, object>()
            {
                {
                    "Level", App.GetLevelData().CurrentLevel.ToString()
                }
            };
//            levelParams[AppEventParameterName.ContentID] = App.GetLevelData().currentLevelShow;
//            levelParams[AppEventParameterName.Description] = "Level "+App.GetLevelData().currentLevelShow.ToString()+" complete.";
//            levelParams[AppEventParameterName.Success] = "1";
            
            FB.LogAppEvent(currentLevel, null, levelParams);
        }
        
        private void FbLogCompletedRewardVideo() 
        {
            FB.LogAppEvent(
                "RewardVideoSeen"
            );
        }
    }
}
#endif
