using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.Timer;
using UnityEngine;

namespace Sourav.Test._Scripts
{
    public class TestOfTimer : GameElement
    {
        [SerializeField] private TimerInfo info;
        
        [Sirenix.OdinInspector.Button("UNSCALED")]
        public void StartUnscaledTimer()
        {
            TimerWatch watch = new TimerWatch();
            TimerInfo infoCurrent = new TimerInfo(info);
            infoCurrent.id = "us";
            infoCurrent.OnTickInt = i => { D.Log($"tickUnscaled = {i}");};
            infoCurrent.OnTickContinous = f => { D.Log($"tickContinuous = {f}");};
            infoCurrent.OnTimerFinished = () => { D.Log($"Timer finished UNSCALED");};
            
            App.GetCoroutineHandler().StartTimer(watch: watch, info: infoCurrent);
        }
        
        [Sirenix.OdinInspector.Button("SCALED")]
        public void StartScaledTimer()
        {
            TimerWatch watch = new TimerWatch();
            TimerInfo infoCurrent = new TimerInfo(info);
            infoCurrent.id = "s";
            infoCurrent.OnTickInt = i => { D.Log($"tickScaled = {i}");};
            infoCurrent.OnTickContinous = f => { D.Log($"tickContinuous = {f}");};
            infoCurrent.OnTimerFinished = () => { D.Log($"Timer finished SCALED");};
            
            App.GetCoroutineHandler().StartTimer(watch: watch, info: infoCurrent);
        }

        [Sirenix.OdinInspector.Button("PAUSE")]
        public void PauseTimer(string id)
        {
            App.GetCoroutineHandler().GetTimer(id)?.PauseTimer();
        }
        [Sirenix.OdinInspector.Button("RESUME")]
        public void ResumeTimer(string id)
        {
            App.GetCoroutineHandler().GetTimer(id)?.ResumeTimer();
        }
        [Sirenix.OdinInspector.Button("STOP")]
        public void StopTimer(string id, bool invokeEndAction)
        {
            App.GetCoroutineHandler().GetTimer(id)?.StopTimer(invokeEndAction);
        }

        [Sirenix.OdinInspector.Button()]
        public void FreezeTime()
        {
            Time.timeScale = 0;
        }
        
        [Sirenix.OdinInspector.Button()]
        public void UnFreezeTime()
        {
            Time.timeScale = 1;
        }

        [Sirenix.OdinInspector.Button()]
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
