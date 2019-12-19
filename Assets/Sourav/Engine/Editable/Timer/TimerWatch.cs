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
            int addSubtractMultiplier = 1;
            if (info.isCountingDown)
            {
                timer = info.duration;
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
                    yield break;
                }
                
                while (_isPaused)
                {
                    yield return null;
                }
                
                if (info.isUnScaled)
                {
                    timerFraction += Time.unscaledDeltaTime;
                }
                else
                {
                    timerFraction += Time.deltaTime;
                }

                if (timerFraction > 1.0f)
                {
                    timer += (1 * addSubtractMultiplier);
                    timerFraction = 0.0f;
                    info.OnTickInt?.Invoke((int) timer);

                    if (info.isCountingDown)
                    {
                        if (timer < 0.0f)
                        {
                            canRunTimer = false;
                        }
                    }
                    else
                    {
                        if (timer > info.duration)
                        {
                            canRunTimer = false;
                        }
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
        public bool isCountingDown;
        public System.Action OnTimerFinished;
    }
}
