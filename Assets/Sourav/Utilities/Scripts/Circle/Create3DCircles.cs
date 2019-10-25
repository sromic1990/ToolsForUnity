using System.Collections.Generic;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.Utilities.Scripts.Circle
{
    public class Create3DCircles : MonoBehaviour
    {
        public static void CreateCircles(List<Vector3> points, GameObject circlePrefab, Transform parent, Vector3 parentPosition)
        {
            parent.position = parentPosition;
            
            for (int i = 0; i < points.Count; i++)
            {
                GameObject go = (GameObject)Instantiate(circlePrefab, points[i], Quaternion.identity);
                go.transform.SetParent(parent);

            }
        }
    }
}
