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
            data.coroutine = StartCoroutine(watch.StartTimer(info, currentIndex, this));
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
    }

    [System.Serializable]
    public class CoroutineData
    {
        public Coroutine coroutine;
        public int id;
    }
}
