using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.Test.IdleGameTest._Scripts.ControllerRelated
{
    public class IdleTestUiController : Sourav.Engine.Core.ControllerRelated.Controller
    {
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.SetUi:
                    SetUpButtons();
                    App.Notify(Notification.UiSet);
                    break;
                
                case Notification.SetUpIdleButtons:
                    SetUpButtons();
                    break;
            }
        }

        private void SetUpButtons()
        {
             
        }
    }
}
