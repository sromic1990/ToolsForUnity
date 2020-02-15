using System.Collections.Generic;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.DataRelated;

namespace Sourav.Engine.Editable.AdditionalLogicRelated
{
    public class LogicEvaluator : GameElement
    {
        public LogicStatus EvaluateLogic(List<LogicData> logicData)
        {
            LogicStatus status = LogicStatus.Fulfilled;

            if (logicData == null)
            {
                return status;
            }

            if (logicData.Count == 0)
            {
                return status;
            }

            if (App.GetLevelData().LogicData == null)
            {
                return status;
            }
            
            for (int i = 0; i < logicData.Count; i++)
            {
                if (logicData[i].type == LogicType.None)
                {
                    continue;
                }
                
                LogicStatus statusOfLogic = App.GetLevelData().GetLogicStatus(logicData[i].type);
                if (statusOfLogic != logicData[i].fulfillmentStatus)
                {
                    status = LogicStatus.Unfulfilled;
                    break;
                }
            }

            return status;
        }
    }
}