using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sourav.UIPresets.PopUpRelated.Element
{
    public class PopUpView : GameElement
    {
        #region FIELDS
        [SerializeField] private GameObject popUpPanel;
        [SerializeField] private List<PopUpHolder> popUpHolders;
        [ReadOnly][SerializeField] private GameObject currentPopUp;
        [ReadOnly] [SerializeField] private PopUpHolder currentHolder;
        [ReadOnly] [SerializeField] private bool isPopUpOpen;
        [SerializeField] private Color panelShowColor;
        [SerializeField] private Color panelDontShowColor;
        #endregion

        #region METHODS
        #region PUBLIC METHODS
        [Sirenix.OdinInspector.Button()]
        public void HidePopUp()
        {
            if (currentPopUp == null)
            {
                HideAllPopUps();
            }
            else
            {
                DOTween.Restart(currentPopUp, "out");
                currentHolder.onPopUpHide?.Invoke();
                currentPopUp = null;
                currentHolder = null;
            }
        }

        [Sirenix.OdinInspector.Button()]
        public void HidePopUpSilently()
        {
            if (currentPopUp == null)
            {
                HideAllPopUps();
            }
            else
            {
                currentPopUp.transform.localScale = Vector3.zero;
                currentPopUp = null;
                OnHidePopUpComplete();
            }
        }

        [Sirenix.OdinInspector.Button()]
        public void ShowPopUp(Model.PopUpType type, bool isPanelShow = true)
        {
            GameObject popUp = GetCorrectPopUp(type);
            if (popUp != null)
            {
                popUpPanel.Show();
                if (isPanelShow)
                {
                    popUpPanel.GetComponent<Graphic>().color = panelShowColor;
                }
                else
                {
                    popUpPanel.GetComponent<Graphic>().color = panelDontShowColor;
                }
                isPopUpOpen = true;
                currentPopUp = popUp;
                currentPopUp.transform.localScale = Vector3.zero;
                currentPopUp.Show();
                DOTween.Restart(currentPopUp, "in");
                currentHolder.onPopUpShow?.Invoke();
            }
        }

        public bool IsPopUpOpen()
        {
            return isPopUpOpen;
        }
        #endregion

        #region PRIVATE METHODS
        private void HideAllPopUps()
        {
            for (int i = 0; i < popUpHolders.Count; i++)
            {
                popUpHolders[i].popUpObject.transform.localScale = Vector3.zero;
                popUpHolders[i].popUpObject.Hide();
            }
            OnHidePopUpComplete();
        }
        #endregion

        #region EVENT METHODS
        public void OnHidePopUpComplete()
        {
            isPopUpOpen = false;
            popUpPanel.Hide();
            App.GetNotificationCenter().Notify(Notification.PopUpHidingComplete);
        }

        public void OnPopUpShowComplete()
        {
            isPopUpOpen = true;
            App.GetNotificationCenter().Notify(Notification.PopUpShown);
        }
        #endregion

        #region HELPER METHODS
        private GameObject GetCorrectPopUp(Model.PopUpType type)
        {
            for (int i = 0; i < popUpHolders.Count; i++)
            {
                if (type == popUpHolders[i].type)
                {
                    currentHolder = popUpHolders[i];
                    return popUpHolders[i].popUpObject;
                }
            }

            return null;
        }
        #endregion
        #endregion
    }

    [System.Serializable]
    public class PopUpHolder
    {
        public Model.PopUpType type;
        public GameObject popUpObject;
        public UnityEvent onPopUpShow;
        public UnityEvent onPopUpHide;
    }
}
