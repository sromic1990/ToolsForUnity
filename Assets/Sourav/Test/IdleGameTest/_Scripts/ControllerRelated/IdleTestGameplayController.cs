#if IDLEGAME
using Sourav.Engine.Core.DebugRelated;
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
                    // D.Log("IdleTestGameplayController -> PlayGame");
                    isGamePlaying = false;
                    App.Notify(Notification.SetUi);
                    break;
                
                case Notification.UiSet:
                    isGamePlaying = true;
                    break;

                case Notification.SecondTick:
                    if(!isGamePlaying)
                        return;
                    if (App.GetLevelData().unitsToBeAddedNextTick > 0)
                    {
                        App.GetLevelData().UnitIncrement += App.GetLevelData().unitsToBeAddedNextTick;
                        App.GetLevelData().unitsToBeAddedNextTick = 0;
                    }
                    App.GetLevelData().Unit += App.GetLevelData().UnitIncrement;
                    App.GetLevelData().unitsFoundInTapThisSecond = 0;
                    App.Notify(Notification.UnitsUpdated);
                    break;
                
                case Notification.TapperPressed:
                    if(!isGamePlaying)
                        return;
                    App.GetLevelData().Unit += App.GetLevelData().UnitPerTap;
                    App.GetLevelData().unitsFoundInTapThisSecond += App.GetLevelData().UnitPerTap;
                    App.Notify(Notification.UnitsUpdated);
                    break;
            }
        }
    }
}
#endif
