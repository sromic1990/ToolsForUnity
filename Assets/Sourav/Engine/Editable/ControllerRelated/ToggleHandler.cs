using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Editable.ToggleRelated;
using Sourav.UIPresets;
using UnityEngine;

namespace Sourav.Engine.Editable.ControllerRelated
{
    public class ToggleHandler : Core.ControllerRelated.Controller
    {
        [SerializeField] private ToggleSwitch[] toggles;
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.GameLoaded:
                    SetToggles();
                    break;
                
                case Notification.ToggleOn:
                    ToggleOnOff((ToggleType)param.intData[0], ToggleStatus.On);
                    break;
                
                case Notification.ToggleOff:
                    ToggleOnOff((ToggleType)param.intData[0], ToggleStatus.Off);
                    break;
            }
        }

        private void SetToggles()
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].GetToggleType() == ToggleType.Music)
                {
                    toggles[i].SetToggle(App.GetLevelData().IsMusicOn);
                }
                else if (toggles[i].GetToggleType() == ToggleType.Vibration)
                {
                    toggles[i].SetToggle(App.GetLevelData().IsVibrationOn);
                }
                else if(toggles[i].GetToggleType() == ToggleType.SFX)
                {
                    toggles[i].SetToggle(App.GetLevelData().IsSfxOn);
                }
            }
        }

        private void ToggleOnOff(ToggleType toggle, ToggleStatus status)
        {
            switch (status)
            {
                case ToggleStatus.On:
                    switch (toggle)
                    {
                        case ToggleType.Music:
                            App.GetLevelData().IsMusicOn = true;
                            break;
                        
                        case ToggleType.Vibration:
                            App.GetLevelData().IsVibrationOn = true;
                            break;
                        
                        case ToggleType.SFX:
                            App.GetLevelData().IsSfxOn = true;
                            break;
                    }
                    break;
                
                case ToggleStatus.Off:
                    switch (toggle)
                    {
                        case ToggleType.Music:
                            App.GetLevelData().IsMusicOn = false;
                            break;
                        
                        case ToggleType.Vibration:
                            App.GetLevelData().IsVibrationOn = false;
                            break;
                        
                        case ToggleType.SFX:
                            App.GetLevelData().IsSfxOn = false;
                            break;
                    }
                    break;
            }
        }
    }
}
