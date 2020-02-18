#if IDLEGAME
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.IdleGameEngine.IdleCurrency.IdleCurrency;
using UnityEngine;
using UnityEngine.UI;

namespace Sourav.Test.IdleGameTest._Scripts.ControllerRelated
{
    public class IdleTestUiController : Sourav.Engine.Core.ControllerRelated.Controller
    {
        [SerializeField] private Text unitText;
        [SerializeField] private Text unitsPerSecondText;
        
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.SetUi:
                    // D.Log("IdleTestUiController -> SetUi");
                    SetUpButtons();
                    break;
                
                case Notification.IdleButtonsGenerated:
                    App.Notify(Notification.UiSet);
                    break;
                
                case Notification.UnitsUpdated:
                    unitText.text = App.GetLevelData().Unit.ToShortString();
                    IdleCurrency perSecond =
                        App.GetLevelData().UnitIncrement + App.GetLevelData().unitsFoundInTapThisSecond;
                    unitsPerSecondText.text = perSecond.ToShortString() + "/sec";
                    break;
                
                case Notification.SetUpIdleButtons:
                    SetUpButtons();
                    break;
            }
        }

        private void SetUpButtons()
        {
             App.Notify(Notification.GenerateIdleButtons);
        }
    }
}
#endif
