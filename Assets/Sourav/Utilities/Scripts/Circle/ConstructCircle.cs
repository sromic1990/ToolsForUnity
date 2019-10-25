using System.Collections.Generic;
using UnityEngine;

namespace Sourav.Utilities.Scripts.Circle
{
    public static class ConstructCircle
    {
        public static List<Vector3> ConstructXZ(float radiusOfCircle, int number, Vector3 startingPosition, float defaultY = 0.0f)
        {
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < number; i++)
            {
                float angle = i * Mathf.PI * 2f / number;
                positions.Add(startingPosition + new Vector3(Mathf.Cos(angle) * radiusOfCircle, defaultY, Mathf.Sin(angle) * radiusOfCircle));
            }
            return positions;
        }
        
        public static List<Vector3> ConstructXY(float radiusOfCircle, int number, Vector3 startingPosition, float defaultZ = 0.0f)
        {
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < number; i++)
            {
                float angle = i * Mathf.PI * 2f / number;
                positions.Add(startingPosition + new Vector3(Mathf.Cos(angle) * radiusOfCircle, Mathf.Sin(angle) * radiusOfCircle, defaultZ));
            }
            return positions;
        }
        
        public static List<Vector3> ConstructYZ(float radiusOfCircle, int number, Vector3 startingPosition, float defaultX = 0.0f)
        {
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < number; i++)
            {
                float angle = i * Mathf.PI * 2f / number;
                positions.Add(startingPosition + new Vector3(defaultX, Mathf.Cos(angle) * radiusOfCircle, Mathf.Sin(angle) * radiusOfCircle));
            }
            return positions;
        }
    }
}
