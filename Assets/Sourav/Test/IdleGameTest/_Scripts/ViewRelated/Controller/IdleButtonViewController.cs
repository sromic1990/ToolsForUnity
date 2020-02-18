#if IDLEGAME
using System.Collections.Generic;
using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.IdleGameEngine.IdleGameData;
using Sourav.Test.IdleGameTest._Scripts.ViewRelated.Elements;
using UnityEngine;

namespace Sourav.Test.IdleGameTest._Scripts.ViewRelated.Controller
{
    public class IdleButtonViewController : Sourav.Engine.Core.ControllerRelated.Controller
    {
        [SerializeField] private List<IdleButtonView> buttonViews;
        [SerializeField] private IdleButtonGenerator generator;
        
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.GenerateIdleButtons:
                    // D.Log("IdleButtonViewController -> GenerateIdleButtons");
                    generator.Generate(App.GetIdleData().idleLevels[App.GetLevelData().CurrentLevel], out buttonViews);
                    break;
                
                case Notification.StartBlockIng:
                    IdleButtonView buttonView = GetIdleButton((IdleUnitType) param.intData[0]);
                    if (buttonView != null)
                    {
                        buttonView.StartFill();
                    }
                    break;
                
                case Notification.ButtonsUpdated:
                    for (int i = 0; i < buttonViews.Count; i++)
                    {
                        buttonViews[i].SetUp(App.GetIdleData().idleLevels[App.GetLevelData().CurrentLevel].idleInfos[i]);
                    }
                    break;
            }
        }

        private IdleButtonView GetIdleButton(IdleUnitType type)
        {
            if (buttonViews == null)
                return null;
            
            for (int i = 0; i < buttonViews.Count; i++)
            {
                if (buttonViews[i].GetUnit() == type)
                {
                    return buttonViews[i];
                }
            }
            
            return null;
        }
    }
}
#endif
