using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;

namespace Sourav.Test.IdleGameTest._Scripts.ControllerRelated
{
    public class IdleTestGameplayController : Sourav.Engine.Core.ControllerRelated.Controller
    {
        private bool isGamePlaying;
        
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.PlayGame:
                    isGamePlaying = false;
                    App.Notify(Notification.SetUi);
                    break;
                
                case Notification.UiSet:
                    isGamePlaying = true;
                    break;

                case Notification.SecondTick:
                    if(!isGamePlaying)
                        return;
                    App.GetLevelData().Unit += App.GetLevelData().UnitIncrement;
                    break;
                
                case Notification.IdleButtonPressed:
                    
                    break;
            }
        }
    }
}
