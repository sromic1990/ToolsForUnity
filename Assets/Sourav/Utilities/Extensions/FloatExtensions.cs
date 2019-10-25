using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sourav.Utilities.Extensions
{
    public static class FloatExtensions
    {
        public static bool Between(this float f, float f_lower, float f_higher)
        {
            if (f < f_higher && f > f_lower)
                return true;
            else return false;
        }
        
        public static float Round(this float f, int roundValue)
        {
            f = (float)System.Math.Round((double) f, roundValue);
            return f;
        }
    }
}
