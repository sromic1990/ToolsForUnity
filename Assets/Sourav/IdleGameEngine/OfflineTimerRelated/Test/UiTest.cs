#if IDLEGAME
using System.Globalization;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;
using UnityEngine.UI;

namespace Sourav.IdleGameEngine.OfflineTimerRelated.Test
{
    public class UiTest : Sourav.Engine.Core.ControllerRelated.Controller
    {
        [SerializeField] private Text timerSeconds;
        
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.OfflineSecondsSet:
                    if (App.GetLevelData().lastDateTimeSeconds > 0)
                    {
                        timerSeconds.text = "Offline seconds " + App.GetLevelData().lastDateTimeSeconds.ToString(CultureInfo.InvariantCulture)+"s";
                    }
                    else
                    {
                        timerSeconds.text = "";
                    }
                    break;
            }
        }
    }
}
#endif
