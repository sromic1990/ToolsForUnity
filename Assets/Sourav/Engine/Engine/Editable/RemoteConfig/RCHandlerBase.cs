using Sirenix.OdinInspector;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.RemoteConfig
{
    public abstract class RCHandlerBase : GameElement
    {
        [SerializeField][ReadOnly] private RCType rcType;

        public virtual void HandleRC(string data, RCType type)
        {
            rcType = type;
        }

        protected void RCSet()
        {
            for (int i = 0; i < App.GetData<RCData>().rcDefaultValues.Length; i++)
            {
                if (App.GetData<RCData>().rcDefaultValues[i].type == rcType)
                {
                    App.GetData<RCData>().rcDefaultValues[i].isFetched = true;
                    break;
                }
            }
            
            App.Notify(Notification.RCFetched);
        }
    }
}