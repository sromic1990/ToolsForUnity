using Sourav.DebugRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;

namespace _Tests
{
    public class MainController : Sourav.Engine.Core.ControllerRelated.Controller
    {
        private void Start()
        {
            App.Notify(Notification.LoadGame);
        }
        
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.GameLoaded:
                    D.Log("GAME LOADED");
                    HandleLoadData();
                    break;
                
                case Notification.TestNotification:
                    DisplayInformation(param);
                    break;
            }
        }

        private void DisplayInformation(NotificationParam param)
        {
            D.Log($"My name is {param.stringData["myName"]}, my age is {param.intData["myAge"]} and I am a " +
                  $"{(param.boolData["isMale"] ? "Male" : "Female")}");
            D.Log($"Another integer is {param.intData["anotherInt"]}");
        }

        private void HandleLoadData()
        {
            // int coins = App.GetData<LevelCommonData>().Coins;
            // D.Log($"Coins = {coins}");

            int dataValue = App.GetData<TestCommonData>().dataValue;
            D.Log($"DataValue = {dataValue}");
            
            TestPoco poco = new TestPoco();
            poco.Test();
        }
    }
}
