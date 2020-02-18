#if IDLEGAME
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;

namespace Sourav.Test.IdleGameTest._Scripts.ControllerRelated
{
    public class IdleTestMainController : Sourav.Engine.Core.ControllerRelated.Controller
    {
        private void Start()
        {
            App.Notify(Notification.LoadGame);
        }
        
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.GameLoaded:
                    App.Notify(Notification.PlayGame);
                    break;
            }
        }
    }
}
#endif
