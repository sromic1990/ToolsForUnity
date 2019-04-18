using System.Collections.Generic;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.UIPresets;
using Sourav.Utilities.Extensions;
using UnityEngine;

namespace Sourav.Engine.Editable.ControllerRelated
{
    public class UiScreenHandler : Controller
    {
        [SerializeField] private UiScreen[] screens;
        private ScreenType screenToClose;
        private ScreenType screenToShow;

        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.ShowScreen:
                    App.GetNotificationCenter().Notify(Notification.PauseGame);
                    screenToShow = (ScreenType)param.intData[0];
                    HideAllScreenExceptTheOneToShow(screenToShow);
                    break;
                
                case Notification.TransitionComplete:
                    if ((ScreenType) param.intData[0] == screenToShow)
                    {
                        screenToShow = ScreenType.None;
                        App.GetNotificationCenter().Notify(Notification.ResumeGame);
                    }
                    else if ((ScreenType) param.intData[0] == screenToClose)
                    {
                        screenToClose = ScreenType.None;
                        ShowScreen(screenToShow);
                    }
                    break;
            }
        }

        private UiScreen GetScreenAsPerScreenType(ScreenType screen)
        {
            UiScreen uiScreen = null;

            for (int i = 0; i < screens.Length; i++)
            {
                if (screens[i].screenType == screen)
                {
                    uiScreen = screens[i];
                    break;
                }
            }
            return uiScreen;
        }

        private void HideAllScreenExceptTheOneToShow(ScreenType exceptScreen)
        {
            for (int i = 0; i < screens.Length; i++)
            {
                if (screens[i].screenType == exceptScreen)
                    continue;

                if (screens[i].gameObject.activeSelf)
                {
                    screenToClose = screens[i].screenType;
                    screens[i].CloseScreen();
                }
            }

            if (screenToClose == ScreenType.None)
            {
                for (int i = 0; i < screens.Length; i++)
                {
                    screens[i].gameObject.Hide();
                }
            }

            ShowScreen(exceptScreen);
        }

        private void ShowScreen(ScreenType showScreen)
        {
            GetScreenAsPerScreenType(showScreen).OpenScreen();
        }
    }
    
    public enum ScreenType
    {
        None,
        MainMenu,
        GamePlay,
        LevelFail,
        LevelComplete,
        Shop,
        Settings,
    }
}
