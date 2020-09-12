using Sourav.Engine.Core.ApplicationRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;

namespace _Tests
{
    public class TestPoco
    {
        public void Test()
        {
            NotificationParam param = new NotificationParam(Mode.intData | Mode.stringData | Mode.boolData);
            param.intData["myAge"] = 29;
            param.intData["anotherInt"] = 1000;
            param.stringData["myName"] = "Sourav";
            param.boolData["isMale"] = true;
            
            App.Notify(Notification.TestNotification, param);
        }
    }
}
