using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using Sourav.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Sourav.Engine.UIPresets.PopUp
{
    public class PopUpView : GameElement, IPopUpView
    {
        [SerializeField] private PopUpType _type;
        public PopUpType Type => _type;
        public Transform popUpTransform => transform;
        
        [SerializeField] private Transform[] _blurBGs;
        [SerializeField] private Transform _bg;
        [SerializeField] private List<PopUpButtonAction> buttons;

        [Space(10)] [Header("Events with popups")]
        public UnityEvent OnResetPopUp;
        public UnityEvent OnPopUpStartingToShow;
        public UnityEvent OnPopUpShown;
        public UnityEvent OnPopUpStartingToHide;
        public UnityEvent OnPopUpHidden;

        private bool _isPopUpReady;
        public bool IsPopUpReady => _isPopUpReady;

        private void Start()
        {
            InitializeButton();
            InitializePopUp();
        }

        private void InitializePopUp()
        {
            OnResetPopUp?.Invoke();
            _isPopUpReady = false;
            for (int i = 0; i < _blurBGs.Length; i++)
            {
                _blurBGs[i].gameObject.Hide();
            }
            _bg.localScale = Vector3.zero;
        }

        private void InitializeButton()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].button = buttons[i].popUpButton.GetComponent<IPopUpButton>();
                buttons[i].button.SetUp(this, buttons[i].actions, OnButtonPressed);
            }
        }


        public void ShowPopUp()
        {
            OnPopUpStartingToShow?.Invoke();

            for (int i = 0; i < _blurBGs.Length; i++)
            {
                _blurBGs[i].gameObject.Show();
            }

            _bg.DOScale(new Vector3(1, 1, 1), 0.5f).SetDelay(0.2f).OnComplete(() =>
            {
                _isPopUpReady = true;
                OnPopUpShown?.Invoke();

                for (int i = 0; i < _blurBGs.Length; i++)
                {
                    _blurBGs[i].gameObject.Show();
                }
            }).SetEase(Ease.OutBack);
        }
        
        public void HidePopUp()
        {
            OnPopUpStartingToHide?.Invoke();
            
            _isPopUpReady = false;
            _bg.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => InitializePopUp()).SetEase(Ease.InBack).OnComplete(
                () =>
                {
                    for (int i = 0; i < _blurBGs.Length; i++)
                    {
                        _blurBGs[i].gameObject.Hide();
                    }
                    
                    OnPopUpHidden?.Invoke();
                    
                    NotificationParam popUp = new NotificationParam(Mode.intData);
                    popUp.intData["type"] = (int)_type;
                    
                    OnResetPopUp?.Invoke();
                    
                    App.Notify(Notification.PopUpHidden, popUp);
                });
        }

        [Button()]
        public void HidePopUpSilently()
        {
            InitializePopUp();
        }

        [Button()]
        public void ShowPopUpSilently()
        {
            for (int i = 0; i < _blurBGs.Length; i++)
            {
                _blurBGs[i].gameObject.Show();
            }
            _bg.localScale = Vector3.one;
            _isPopUpReady = true;
        }

        private void OnButtonPressed(PopUpButtonActions action)
        {
            NotificationParam buttonPressedParam = new NotificationParam(Mode.intData);
            buttonPressedParam.intData["type"] = (int) _type;
            buttonPressedParam.intData["action"] = (int) action;
            App.Notify(Notification.PopUpButtonPressed, buttonPressedParam);
        }
    }

    [System.Serializable]
    public class PopUpButtonAction
    {
        public GameObject popUpButton;
        public IPopUpButton button;
        public List<PopUpButtonActions> actions;
    }
}
