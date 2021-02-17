using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.TestingRelated
{
    public class FireAnyNotification : GameElement
    {
        [SerializeField] private Notification notification;
        [SerializeField] private NotificationParam param;

        // [Sirenix.OdinInspector.Button()]
        private void FireNotification()
        {
            App.Notify(notification, param);
        }
    }
}
