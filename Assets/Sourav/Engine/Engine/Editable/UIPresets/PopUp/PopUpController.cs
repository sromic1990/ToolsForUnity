using System.Collections.Generic;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using UnityEngine;

namespace Sourav.Engine.UIPresets.PopUp
{
    public class PopUpController : Core.ControllerRelated.Controller
    {
        private List<IPopUpView> popUpViews;
        
        private void Awake()
        {
            InitializePopUpViews();
        }

        private void InitializePopUpViews()
        {
            popUpViews = new List<IPopUpView>();
            for (int i = 0; i < transform.childCount; i++)
            {
                IPopUpView popUpView = transform.GetChild(i).GetComponent<IPopUpView>();
                if (popUpView != null)
                {
                    popUpViews.Add(popUpView);
                }
            }
        }

        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.LoadGame:
                    HideAllPopUps();
                    break;

                case Notification.ShowPopup:
                    ShowPopUp((PopUpType)param.intData["popUpType"]);
                    break;
                
                case Notification.HidePopUp:
                    HidePopup((PopUpType)param.intData["popUpType"], param.boolData["isHidingSilent"]);
                    break;
                
                case Notification.HideAllPopUps:
                    HideAllPopUps();
                    break;
                
                case Notification.PopUpButtonPressed:
                    OnPopUpButtonPressed(
                        (PopUpType) param.intData["type"],
                        (PopUpButtonActions) param.intData["action"]);
                    break;
                
                case Notification.PopUpHidden:
                    break;
                
                case Notification.SetUi:
                    HideAllPopUps();
                    break;
                
                // case Notification.ChangeStage:
                //     HidePopup(PopUpType.StageComplete);
                //     break;
            }
        }

        private void ShowPopUp(PopUpType type)
        {
            IPopUpView popUpView = GetCorrectPopUp(type);
            if (popUpView != null)
            {
                BringForth(popUpView.popUpTransform);
                popUpView.ShowPopUp();

                if (!App.GetData<PopUpData>().openPopUpsList.Contains(type))
                {
                    App.GetData<PopUpData>().openPopUpsList.Add(type);
                }
            }
        }

        private void HidePopup(PopUpType type, bool hideSilently = false)
        {
            IPopUpView popUpView = GetCorrectPopUp(type);
            if (popUpView != null)
            {
                if (hideSilently)
                {
                    popUpView.HidePopUpSilently();
                }
                else
                {
                    popUpView.HidePopUp();
                }
                
                if (App.GetData<PopUpData>().openPopUpsList.Contains(type))
                {
                    App.GetData<PopUpData>().openPopUpsList.Remove(type);
                }
            }
        }

        private void HideAllPopUps()
        {
            if (popUpViews == null)
            {
                return;
            }
        
            for (int i = 0; i < popUpViews.Count; i++)
            {
                popUpViews[i].HidePopUpSilently();
            }
            App.GetData<PopUpData>().openPopUpsList = new List<PopUpType>();
        }
        
        private void OnPopUpButtonPressed(PopUpType popUpType, PopUpButtonActions popUpButtonActions)
        {
            switch (popUpButtonActions)
            {
                case PopUpButtonActions.ClosePopUp:
                    HidePopup(popUpType);
                    break;
            }   
        }

        public IPopUpView GetCorrectPopUp(PopUpType type)
        {
            for (int i = 0; i < popUpViews.Count; i++)
            {
                if (popUpViews[i].Type == type)
                    return popUpViews[i];
            }
            return null;
        }
        
        public void BringForth(Transform childTransform)
        {
            int lastSiblingIndex = transform.childCount - 1;
            childTransform.SetSiblingIndex(lastSiblingIndex);
        }
    }

    public enum PopUpType
    {
        None,
        Settings,
        OfflinePopUp,
        Upgrades,
        Store,
        Book,
        HistoryUnlocked,
        TimeTravelFree,
        TimeTravelAnimation,
        TimeTravelComplete,
        FRV,
        NotEnoughDiamonds,
        DiamondsAdded,
        PurchaseFailed,
        AdsRemoved,
        PurchaseRestored,
        DiamondsTTCombo,
    }
}
