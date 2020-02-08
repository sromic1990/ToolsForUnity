using Sirenix.OdinInspector;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.IdleGameEngine.TickerRelated
{
    public class Ticker : Sourav.Engine.Core.ControllerRelated.Controller
    {
        [ReadOnly][SerializeField]private float currentSecondFraction;
        
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.Update:
                    HandleUpdatePassed();
                    break;
            }
        }

        private void HandleUpdatePassed()
        {
            currentSecondFraction += Time.unscaledDeltaTime;
            if (currentSecondFraction > 1.0f)
            {
                currentSecondFraction = 0.0f;
                App.GetNotificationCenter().Notify(Notification.SecondTick);
            }
        }
    }
}
