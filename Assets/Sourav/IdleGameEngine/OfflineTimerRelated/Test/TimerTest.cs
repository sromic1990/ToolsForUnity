using System.Globalization;
using Sirenix.OdinInspector;
using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.IdleGameEngine.OfflineTimerRelated.Test
{
    public class TimerTest : GameElement
    {
        [ReadOnly][SerializeField] private string dateTime1;
        [ReadOnly][SerializeField] private string dateTime2;
        [ReadOnly][SerializeField] private bool storeFirst;
        
        [Sirenix.OdinInspector.Button()]
        public void ShowDateTime()
        {
            D.Log("DateTime = "+System.DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }

        [Sirenix.OdinInspector.Button()]
        public void StoreDateTime()
        {
            if (!storeFirst)
            {
                D.Log("DateTime = "+System.DateTime.Now.ToString(CultureInfo.InvariantCulture));
                dateTime1 = System.DateTime.Now.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                D.Log("DateTime = "+System.DateTime.Now.ToString(CultureInfo.InvariantCulture));
                dateTime2 = System.DateTime.Now.ToString(CultureInfo.InvariantCulture);
            }

            storeFirst = !storeFirst;
        }

        [Sirenix.OdinInspector.Button()]
        public void CalculateElapsedTime()
        {
            System.DateTime d1 = System.DateTime.Parse(dateTime1);
            System.DateTime d2 = System.DateTime.Parse(dateTime2);

            System.TimeSpan t = d2 - d1;
            D.Log("Elapsed = "+t.TotalSeconds);
        }
    }
}
