using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sourav.Extensions
{
    public static class FloatExtensions
    {
        public static bool Between(this float f, float f_lower, float f_higher)
        {
            float higher = Mathf.Max(f_lower, f_higher);
            float lower = Mathf.Min(f_lower, f_higher);
            
            if (f < higher && f > lower)
                return true;
            else return false;
        }
        
        public static float Remap (this float value, float from1, float to1, float from2, float to2) 
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}
