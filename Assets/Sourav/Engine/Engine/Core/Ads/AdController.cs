using Sourav.DebugRelated;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.Engine.Engine.Core.Ads
{
    [RequireComponent(typeof(IAdProvider))]
    public class AdController : Sourav.Engine.Core.ControllerRelated.Controller
    {
        private IAdProvider _adProvider;

        private void Start()
        {
            _adProvider = GetComponent<IAdProvider>();
            _adProvider.Initialize();
        }
        

        //1) LISTEN TO AND ACT AS PER ADS SPECIFIC NOTIFICATIONS
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.ShowBanner:
                    _adProvider.ShowBanner();
                    break;
                
                case Notification.HideBanner:
                    _adProvider.HideBanner();
                    break;

                case Notification.ShowInterstitial:
                    D.Log("SHOW FS");
                    _adProvider.ShowFS();
                    break;

                case Notification.ShowRewardVideo:
                    D.Log("SHOW RV");
                    _adProvider.ShowRV();
                    break;
                
                case Notification.ShowDebugger:
                    _adProvider.ShowDebugger();
                    break;
            }
        }
    }
}
