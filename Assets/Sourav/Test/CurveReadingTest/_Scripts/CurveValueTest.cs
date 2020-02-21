using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.CurveRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;

namespace Sourav.Test.CurveReadingTest._Scripts
{
    public class CurveValueTest : GameElement
    {
        [SerializeField] private CurveReading curve;
        [SerializeField] private Vector2 values;

        [Sirenix.OdinInspector.Button()]
        private void GetValueAt(float time)
        {
            time = Mathf.Clamp01(time);
            float value = curve.GetValue(time);

            float finalValue = value.Remap(0, 1, values.x, values.y);
            
            D.Log($"finalValue = {finalValue}");
        }
    }
}
