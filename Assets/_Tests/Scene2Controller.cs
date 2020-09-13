using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;

namespace _Tests
{
    public class Scene2Controller : Sourav.Engine.Core.ControllerRelated.Controller
    {
        private void Start()
        {
            App.Notify(Notification.GameLoaded);
        }
        
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.GameLoaded:
                    // D.Log("Game loaded Scene 2");
                    // D.Log($"Scene 2 data = {App.GetData<Scene2CommonData>().testData2}");
                    break;
            }
        }
    }
}
