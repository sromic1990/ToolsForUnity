using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.CurveRelated
{
    public class CurveReading : GameElement
    {
        [SerializeField] private AnimationCurve curve;

        [Sirenix.OdinInspector.Button()]
        public float GetValue(float timeOfCurve)
        {
            timeOfCurve = Mathf.Clamp01(timeOfCurve);
            float result = curve.Evaluate(timeOfCurve);
            D.Log($"Value = {result}");
            
            return result;
        }
    }
}
