using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Sourav.UIPresets
{
    public class Timer : GameElement
    {
        private float _timerValue;
        private float _maxTimerValue;
        private bool _initialValueSet;
        private bool _runTimer;
        private bool _isCountdown;
        
        [SerializeField] private GameObject timerObject;
        [SerializeField] private Image fillImage;
        [SerializeField] private string objectShowId;
        [SerializeField] private string objectHideId;
        [SerializeField] private GameObject[] hideAfterTimerObjects;
        [SerializeField] private List<Notification> notificationsOnTimerStarted;
        [SerializeField] private List<Notification> notificationsOnHideTimer;
        [SerializeField] private List<Text> timerTexts;

        [Space(10)] [Header("Timer wait time related")] [SerializeField]
        private float waitBeforeHidingTimer;
        
        public void Show(float maxTimerValue, bool isCountdown, List<Notification> notificationsOnTimerStart = null, List<Notification> notificationsOnTimerHidden = null)
        {
            SetUpInitialValues(maxTimerValue, isCountdown, notificationsOnTimerStart, notificationsOnTimerHidden);
            if (timerObject != null)
            {
                DOTween.Restart(timerObject, objectShowId);
            }
            else
            {
               OnTimerShown();
            }
        }

        public void OnTimerShown()
        {
            StartTimer();
        }

        private void SetUpInitialValues(float maxTimerValue, bool isCountDown, List<Notification> notificationsOnTimerStart, List<Notification> notificationsOnTimerHidden)
        {
            this._maxTimerValue = maxTimerValue;
            this._isCountdown = isCountDown;
            notificationsOnTimerStarted = notificationsOnTimerStart;
            notificationsOnHideTimer = notificationsOnTimerHidden;
            _runTimer = false;
            _initialValueSet = true;
        }

        public void StartTimer(float maxValue = 0, bool isCountDown = false, List<Notification> notificationsOnTimerStart = null, List<Notification> notificationsOnTimerHidden = null)
        {
            if (!_initialValueSet)
            {
                SetUpInitialValues(maxValue, isCountDown, notificationsOnTimerStart, notificationsOnTimerHidden);
            }
            ResetTimer(_isCountdown, _maxTimerValue);
            _runTimer = true;
            if (notificationsOnTimerStarted != null)
            {
                for (int i = 0; i < notificationsOnTimerStarted.Count; i++)
                {
                    App.GetNotificationCenter().Notify(notificationsOnTimerStarted[i]);
                }
            }
        }

        private void ResetTimer(bool isCountDown, float maxTimerValue)
        {
            _timerValue = isCountDown ? maxTimerValue : 0;
        }

        private void Update()
        {
            if (!_runTimer)
                return;
            
            if (_isCountdown)
            {
                _timerValue -= Time.unscaledDeltaTime;
                UpdateTimerParameters();
                if (_timerValue <= 0.0f)
                {
                    TimerComplete();
                }
            }
            else
            {
                _timerValue += Time.unscaledDeltaTime;
                UpdateTimerParameters();
                if (_timerValue >= _maxTimerValue)
                {
                    TimerComplete();
                }
            }
        }

        private void UpdateTimerParameters()
        {
            SetUpTimerTexts();
            SetUpImageFill();
        }

        private void SetUpTimerTexts()
        {
            if (timerTexts != null)
            {
                for (int i = 0; i < timerTexts.Count; i++)
                {
                    timerTexts[i].text = _timerValue.ToString();
                }
            }
        }

        private void SetUpImageFill()
        {
            fillImage.fillAmount = _timerValue / _maxTimerValue;
        }

        private void SetUpImageFill(float fillAmount)
        {
            fillImage.fillAmount = fillAmount;
        }

        private void TimerComplete()
        {
            _runTimer = false;
            if (_isCountdown)
            {
                SetUpImageFill(0);
            }

            else
            {
                SetUpImageFill(1);
            }
            
            StartCoroutine(EndTimer());
        }

        private IEnumerator EndTimer()
        {
            yield return new WaitForSecondsRealtime(waitBeforeHidingTimer);
            HideTimer();
        }

        public void Hide()
        {
            HideTimer();
        }
        
        private void HideTimer()
        {
            if (_runTimer)
            {
                _runTimer = false;
            }
            
            if (timerObject != null)
            {
                DOTween.Restart(timerObject, objectHideId);
            }
            else
            {
                OnTimerHidden();
            }
        }

        public void OnTimerHidden()
        {
            for (int i = 0; i < hideAfterTimerObjects.Length; i++)
            {
                hideAfterTimerObjects[i].Hide();
            }
            
            if (notificationsOnHideTimer != null)
            {
                for (int i = 0; i < notificationsOnHideTimer.Count; i++)
                {
                    App.GetNotificationCenter().Notify(notificationsOnHideTimer[i]);
                }
            }
        }

        public void PauseTimer()
        {
            _runTimer = false;
        }

        public void ResumeTimer()
        {
            _runTimer = true;
        }
    }
}
