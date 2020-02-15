using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;

namespace Sourav.Engine.Editable.AdditionalLogicRelated
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
                        if (App.GetLevelData().LogicData != null)
                        {
                            for (int i = 0; i < App.GetLevelData().LogicData.Count; i++)
                            {
                                App.GetLevelData().LogicData[i].fulfillmentStatus = LogicStatus.Unfulfilled;
                            }
                            App.GetLevelData().IsLogicPopulated = true;
                        }
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
                    App.GetLevelData().LogicData[i].fulfillmentStatus = logicStatus;
                }
            }
            App.Notify(Notification.LogicUpdated);
        }
    }
}
