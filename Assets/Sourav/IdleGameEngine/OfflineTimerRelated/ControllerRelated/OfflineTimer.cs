#if IDLEGAME
using System;
using System.Globalization;
using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;

namespace Sourav.IdleGameEngine.OfflineTimerRelated.ControllerRelated
{
    public class OfflineTimer : Sourav.Engine.Core.ControllerRelated.Controller
    {
        private bool _isTimerHandled;
        
        private void OnApplicationPause(bool isPaused)
        {
            if (!isPaused)
            {
                HandleOfflineTime();
            }
            else
            {
                _isTimerHandled = false;
            }
        }

        private void HandleOfflineTime()
        {
            App.GetLevelData().lastDateTimeSeconds = 0;
            if (App.GetLevelData().isLoaded)
            { 
                if (App.GetLevelData().IsOfflineTimerOn)
                {
                    if (App.GetLevelData().LastDateTime != "" && !string.IsNullOrEmpty(App.GetLevelData().LastDateTime))
                    {
                        DateTime now = DateTime.Now;

                        DateTime last = DateTime.Parse(App.GetLevelData().LastDateTime, CultureInfo.InvariantCulture);

                        TimeSpan span = now - last;

                        D.Log("span seconds = "+span.Seconds);
                        D.Log("span minutes = "+span.Minutes);
                        D.Log("span hours = "+span.Hours);
                        D.Log("span days = "+span.Days);

                        App.GetLevelData().lastDateTimeSeconds = span.Seconds + (span.Minutes * 60) + (span.Hours * 60 * 60) + (span.Days * 24 * 60 * 60);
                    }
                    else
                    {
                        App.GetLevelData().LastDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    }
                    App.GetNotificationCenter().Notify(Notification.OfflineSecondsSet);
                }
                _isTimerHandled = true;
            }
        }

        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.GameLoaded:
                    if (!_isTimerHandled)
                    {
                        HandleOfflineTime();
                    }
                    break;
                
                case Notification.SecondTick:
                    if (_isTimerHandled)
                    {
                        App.GetLevelData().LastDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    }
                    break;
            }
        }
    }
}
#endif
