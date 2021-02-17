using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.Timer;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using UnityEngine;

namespace Sourav.Test._Scripts
{
    public class TestOfTimer : GameElement
    {
        [SerializeField] private TimerInfo info;
        
        public void StartUnscaledTimer()
        {
            TimerWatch watch = new TimerWatch();
            TimerInfo infoCurrent = new TimerInfo(info);
            infoCurrent.id = "us";
            // infoCurrent.OnTickInt = i => { D.Log($"tickUnscaled = {i}");};
            // infoCurrent.OnTickContinous = f => { D.Log($"tickContinuous = {f}");};
            // infoCurrent.OnTimerFinished = () => { D.Log($"Timer finished UNSCALED");};
            
            App.GetCoroutineHandler().StartTimer(watch: watch, info: infoCurrent);
        }
        
        public void StartScaledTimer()
        {
            TimerWatch watch = new TimerWatch();
            TimerInfo infoCurrent = new TimerInfo(info);
            infoCurrent.id = "s";
            // infoCurrent.OnTickInt = i => { D.Log($"tickScaled = {i}");};
            // infoCurrent.OnTickContinous = f => { D.Log($"tickContinuous = {f}");};
            // infoCurrent.OnTimerFinished = () => { D.Log($"Timer finished SCALED");};
            
            App.GetCoroutineHandler().StartTimer(watch: watch, info: infoCurrent);
        }

        public void PauseTimer(string id)
        {
            App.GetCoroutineHandler().GetTimer(id)?.PauseTimer();
        }
        public void ResumeTimer(string id)
        {
            App.GetCoroutineHandler().GetTimer(id)?.ResumeTimer();
        }
        public void StopTimer(string id, bool invokeEndAction)
        {
            App.GetCoroutineHandler().GetTimer(id)?.StopTimer(invokeEndAction);
        }

        public void FreezeTime()
        {
            Time.timeScale = 0;
        }
        
        public void UnFreezeTime()
        {
            Time.timeScale = 1;
        }

        public void ExampleOfWait()
        {
            Debug.Log("This is before wait");
            TimerWatch watch = new TimerWatch();
            TimerInfo info = new TimerInfo(this.info);
            info.id = "Example";
            info.OnTickInt = i => { Debug.Log($"Tick : {i}"); };
            info.OnTimerFinished = () => { Debug.Log("Timer Finished"); };
            App.GetCoroutineHandler().StartTimer(info, watch);
        }
    }
}
