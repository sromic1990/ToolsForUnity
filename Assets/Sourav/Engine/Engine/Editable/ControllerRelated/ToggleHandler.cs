using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Editable.ToggleRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
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
                    ToggleOnOff((ToggleType)param.intData["toggle"], ToggleStatus.On);
                    break;
                
                case Notification.ToggleOff:
                    ToggleOnOff((ToggleType)param.intData["toggle"], ToggleStatus.Off);
                    break;
            }
        }

        private void SetToggles()
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].GetToggleType() == ToggleType.Music)
                {
                    toggles[i].SetToggle(Engine.Core.ApplicationRelated.App.GetData<LevelCommonData>().IsMusicOn);
                }
                else if (toggles[i].GetToggleType() == ToggleType.Vibration)
                {
                    toggles[i].SetToggle(Engine.Core.ApplicationRelated.App.GetData<LevelCommonData>().IsVibrationOn);
                }
                else if(toggles[i].GetToggleType() == ToggleType.SFX)
                {
                    toggles[i].SetToggle(Engine.Core.ApplicationRelated.App.GetData<LevelCommonData>().IsSfxOn);
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
                            Engine.Core.ApplicationRelated.App.GetData<LevelCommonData>().IsMusicOn = true;
                            break;
                        
                        case ToggleType.Vibration:
                            Engine.Core.ApplicationRelated.App.GetData<LevelCommonData>().IsVibrationOn = true;
                            break;
                        
                        case ToggleType.SFX:
                            Engine.Core.ApplicationRelated.App.GetData<LevelCommonData>().IsSfxOn = true;
                            break;
                    }
                    break;
                
                case ToggleStatus.Off:
                    switch (toggle)
                    {
                        case ToggleType.Music:
                            Engine.Core.ApplicationRelated.App.GetData<LevelCommonData>().IsMusicOn = false;
                            break;
                        
                        case ToggleType.Vibration:
                            Engine.Core.ApplicationRelated.App.GetData<LevelCommonData>().IsVibrationOn = false;
                            break;
                        
                        case ToggleType.SFX:
                            Engine.Core.ApplicationRelated.App.GetData<LevelCommonData>().IsSfxOn = false;
                            break;
                    }
                    break;
            }
        }
    }
}
