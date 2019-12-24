using System.Collections.Generic;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Utilities.Scripts.Timer;
using UnityEngine;

namespace Sourav.Engine.Editable.Timer
{
    public class CoroutineHandler : GameElement
    {
        public List<CoroutineData> coroutines;
        public int currentIndex;
        
        public void StartTimer(TimerInfo info, TimerWatch watch)
        {
            if (coroutines == null)
            {
                coroutines = new List<CoroutineData>();
                currentIndex = 0;
            }

            if (coroutines.Count == 0)
            {
                currentIndex = 0;
            }
            
            CoroutineData data = new CoroutineData();
            data.id = currentIndex;
            data.info = info;
            data.watch = watch;
            data.coroutine = StartCoroutine(watch.StartTimer(info, currentIndex, this));
            currentIndex++;
            coroutines.Add(data);
        }

        public void CoroutineFinished(int id)
        {
            int index = -1;
            for (int i = 0; i < coroutines.Count; i++)
            {
                if (id == coroutines[i].id)
                {
                    index = i;
                }
            }

            if (index >= 0)
            {
                coroutines.RemoveAt(index);
            }
        }

        public void StopCoroutine(string id, bool oncompleteActionFire)
        {
            for (int i = 0; i < coroutines.Count; i++)
            {
                if (coroutines[i].info.id == id)
                {
                    coroutines[i].StopTimer(oncompleteActionFire);
                }
            }
        }

        public CoroutineData GetTimer(string id)
        {
            for (int i = 0; i < coroutines.Count; i++)
            {
                if (coroutines[i].info.id == id)
                {
                    return coroutines[i];
                }
            }

            return null;
        }
    }

    [System.Serializable]
    public class CoroutineData
    {
        public Coroutine coroutine;
        public int id;
        public TimerInfo info;
        public TimerWatch watch;
        
        public void PauseTimer()
        {
            watch.PauseTimer();
        }

        public void ResumeTimer()
        {
            watch.ResumeTimer();
        }

        public void StopTimer(bool invokeEndActions)
        {
            watch.StopCoroutine(invokeEndActions);
        }
    }
}
