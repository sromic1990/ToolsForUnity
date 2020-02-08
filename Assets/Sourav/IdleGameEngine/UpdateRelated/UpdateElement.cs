using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.NotificationRelated;

namespace Sourav.IdleGameEngine.UpdateRelated
{
    public class UpdateElement : GameElement
    {
        // Update is called once per frame
        private void Update()
        {
            App.GetNotificationCenter().Notify(Notification.Update);
        }

        private void LateUpdate()
        {
            App.GetNotificationCenter().Notify(Notification.LateUpdate);
        }

        private void FixedUpdate()
        {
            App.GetNotificationCenter().Notify(Notification.FixedUpdate);
        }
    }
}
