using System.Collections;
using DG.Tweening;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.ControllerRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;

namespace Sourav.UIPresets
{
    public class UiScreen : GameElement
    {
        public ScreenType screenType;
        [SerializeField] private UiScreenElements[] showOrderElements;
        [SerializeField] private UiScreenElements[] hideOrderElements;
        public bool isPopUp;

        public void CloseScreen()
        {
            ShowHideElements(hideOrderElements, ShowHideAction.Hide);
        }

        public void OpenScreen()
        {
            ShowHideElements(showOrderElements, ShowHideAction.Show);
        }
        
        private void ShowHideElements(UiScreenElements[] uiScreenElements, ShowHideAction showHideAction)
        {
            StartCoroutine(StartAction(uiScreenElements, showHideAction));
        }

        private IEnumerator StartAction(UiScreenElements[] uiScreenElements, ShowHideAction showHideAction)
        {
            for (int i = 0; i < uiScreenElements.Length; i++)
            {
                for (int j = 0; j < uiScreenElements[i].elements.Length; j++)
                {
                    yield return new WaitForSecondsRealtime(uiScreenElements[i].elements[j].waitBefore);
                    for (int k = 0; k < uiScreenElements[i].elements[j].elements.Length; k++)
                    {
                        if (uiScreenElements[i].elements[j].hasEffectWord)
                        {
                            LetDoTweenHandleShowHide(uiScreenElements[i].elements[j].elements[k], uiScreenElements[i].elements[j].effectWord);
                        }
                        else
                        {
                            switch (showHideAction)
                            {
                                case ShowHideAction.Show:
                                    uiScreenElements[i].elements[j].elements[k].Show();
                                    break;
                                
                                case ShowHideAction.Hide:
                                    uiScreenElements[i].elements[j].elements[k].Hide();
                                    break;
                            }
                        }
                        yield return new WaitForSecondsRealtime(uiScreenElements[i].elements[j].waitInBetween);
                    }
                    yield return new WaitForSecondsRealtime(uiScreenElements[i].elements[j].waitAfter);
                }
            }
            NotificationParam param = new NotificationParam(Mode.intData);
            param.intData.Add((int)screenType);
            App.GetNotificationCenter().Notify(Notification.TransitionComplete, param);
        }

        private void LetDoTweenHandleShowHide(GameObject element, string activeWord)
        {
            if (!element.activeSelf)
            {
                element.Show();
            }
            DOTween.Restart(element, activeWord);
        }
    }

    [System.Serializable]
    public class UiScreenElements
    {
        public UiElement[] elements;
    }
    
    [System.Serializable]
    public class UiElement
    {
        public float waitBefore;
        public float waitInBetween;
        public float waitAfter;
        public GameObject[] elements;
        public bool hasEffectWord;
        public string effectWord;
    }

    public enum ShowHideAction
    {
        Show,
        Hide
    }
}
