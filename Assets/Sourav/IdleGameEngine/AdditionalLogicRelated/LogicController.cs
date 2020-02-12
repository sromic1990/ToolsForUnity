using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;

namespace Sourav.IdleGameEngine.AdditionalLogicRelated
{
    public class LogicController : Sourav.Engine.Core.ControllerRelated.Controller
    {
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.GameLoaded:
                    if (!App.GetLevelData().IsLogicPopulated)
                    {
                        for (int i = 0; i < App.GetLevelData().LogicData.Count; i++)
                        {
                            App.GetLevelData().LogicData[i].status = LogicStatus.Unfulfilled;
                        }
                        App.GetLevelData().IsLogicPopulated = true;
                    }
                    break;
                
                case Notification.UpdateLogic:
                    HandleLogicUpdate((LogicType) param.intData[0], (LogicStatus) param.intData[1]);
                    break;
            }
        }

        private void HandleLogicUpdate(LogicType logicType, LogicStatus logicStatus)
        {
            for (int i = 0; i < App.GetLevelData().LogicData.Count; i++)
            {
                if (App.GetLevelData().LogicData[i].type == logicType)
                {
                    App.GetLevelData().LogicData[i].status = logicStatus;
                }
            }
            App.Notify(Notification.LogicUpdated);
        }
    }
}
