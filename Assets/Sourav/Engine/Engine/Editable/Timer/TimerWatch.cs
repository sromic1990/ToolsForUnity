using System.Collections;
using UnityEngine;

namespace Sourav.Engine.Editable.Timer
{
    public class TimerWatch
    {
        private bool _isPaused;
        private bool _breakFromLoop;
        private bool _invokeEndActions;

        public void PauseTimer()
        {
            _isPaused = true;
        }

        public void ResumeTimer()
        {
            _isPaused = false;
        }

        public void StopCoroutine(bool invokeEndActions = true)
        {
            _invokeEndActions = invokeEndActions;
            _breakFromLoop = true;
        }

        public IEnumerator StartTimer(TimerInfo info, int id, CoroutineHandler handler)
        {
            float timer = 0.0f;
            float fractionTimer = 0.0f;

            float continousTimer = 0.0f;
            
            int addSubtractMultiplier = 1;
            if (info.isCountingDown)
            {
                timer = info.duration;
                continousTimer = info.duration;
                addSubtractMultiplier = -1;
            }
            bool canRunTimer = true;
            float timerFraction = 0.0f;
            _isPaused = false;
            _breakFromLoop = false;
            _invokeEndActions = true;

            while (canRunTimer)
            {
                if (_breakFromLoop)
                {
                    if (_invokeEndActions)
                    {
                        info.OnTimerFinished?.Invoke();
                    }
                    handler.CoroutineFinished(id);
                    yield break;
                }
                
                while (_isPaused)
                {
                    yield return null;
                }
                
                if (info.isUnScaled)
                {
                    timerFraction += Time.unscaledDeltaTime;
                    fractionTimer += Time.unscaledDeltaTime;
                    continousTimer += (Time.unscaledDeltaTime * addSubtractMultiplier);
                    if (addSubtractMultiplier == -1 && continousTimer >= 0)
                    {
                        info.OnTickContinous?.Invoke(continousTimer);
                    }
                    else if (addSubtractMultiplier == 1 && continousTimer <= info.duration)
                    {
                        info.OnTickContinous?.Invoke(continousTimer);
                    }

                }
                else
                {
                    timerFraction += Time.deltaTime;
                    fractionTimer += Time.deltaTime;
                    continousTimer += (Time.deltaTime * addSubtractMultiplier);
                    if (addSubtractMultiplier == -1 && continousTimer >= 0)
                    {
                        info.OnTickContinous?.Invoke(continousTimer);
                    }
                    else if (addSubtractMultiplier == 1 && continousTimer <= info.duration)
                    {
                        info.OnTickContinous?.Invoke(continousTimer);
                    }
                }

                info.currentDuration = fractionTimer;

                if (timerFraction > 1.0f)
                {
                    timer += (1 * addSubtractMultiplier);
                    timerFraction = 0.0f;
                    info.OnTickInt?.Invoke((int) timer);
                }

                if (fractionTimer >= info.duration)
                {
                    canRunTimer = false;
                    if (addSubtractMultiplier == -1)
                    {
                        info.OnTickContinous?.Invoke(0);
                    }
                    else if (addSubtractMultiplier == 1)
                    {
                        info.OnTickContinous?.Invoke(info.duration);
                    }
                }

                yield return null;
            }

            if (_invokeEndActions)
            {
                info.OnTimerFinished?.Invoke();
            }
            
            handler.CoroutineFinished(id);
        }
    }
    
    [System.Serializable]
    public class TimerInfo
    {
        public float duration;
        public bool isUnScaled;
        public System.Action<int> OnTickInt;
        public System.Action<float> OnTickContinous;
        public bool isCountingDown;
        public System.Action OnTimerFinished;
        public string id;
        public float currentDuration;

        public TimerInfo(TimerInfo info)
        {
            duration = info.duration;
            isUnScaled = info.isUnScaled;
            OnTickInt = info.OnTickInt;
            OnTickContinous = info.OnTickContinous;
            isCountingDown = info.isCountingDown;
            OnTimerFinished = info.OnTimerFinished;
            id = info.id;
            currentDuration = 0;
        }

        public TimerInfo()
        { }
    }
}
