using System.Collections.Generic;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.UIPresets;
using Sourav.Utilities.Extensions;
using UnityEngine;

namespace Sourav.Engine.Editable.ControllerRelated
{
    public class UiScreenHandler : Core.ControllerRelated.Controller
    {
        [SerializeField] private UiScreen[] screens;
        private ScreenType screenToClose;
        private ScreenType screenToShow;
        private GameObject popUpPanel;
        private bool isPopUpScreen;
        private ScreenType currentOpenedPopUp;

        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.ShowScreen:
                    App.GetNotificationCenter().Notify(Notification.PauseGame);
                    screenToShow = (ScreenType)param.intData[0];
                    UiScreen screen = GetScreenAsPerScreenType(screenToShow);
                    if (!screen.isPopUp)
                    {
                        HideAllScreenExceptTheOneToShow(screenToShow);
                    }
                    else
                    {
                        popUpPanel.gameObject.Show();
                        currentOpenedPopUp = screen.screenType;
                        ShowScreen(screenToShow);
                    }
                    break;
                
                case Notification.TransitionComplete:
                    if (!isPopUpScreen)
                    {
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
                    }
                    else //ScreenToShow == ScreenToClose == ScreenType.None, PopUp
                    {
                       GetScreenAsPerScreenType(screenToClose).gameObject.Hide();
                       popUpPanel.gameObject.Hide();
                       screenToClose = ScreenType.None;
                       isPopUpScreen = false;
                       currentOpenedPopUp = ScreenType.None;
                       App.GetNotificationCenter().Notify(Notification.ResumeGame);
                    }
                    break;
                
                case Notification.HidePopUpScreen:
                    App.GetNotificationCenter().Notify(Notification.PauseGame);
                    screenToClose = currentOpenedPopUp;
                    isPopUpScreen = true;
                    GetScreenAsPerScreenType(screenToClose).CloseScreen();
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
