using Sourav.Utilities.Scripts.Algorithms.Sorting;
using Sourav.Utilities.Scripts.Attributes;
using UnityEngine;

namespace Sourav.Test._Scripts
{
    public class SortTest : MonoBehaviour
    {
        public int[] IntegerArray;

        [Button()]
        public void Sort()
        {
            CountingSort_Integer.Sort(IntegerArray, -1000000, 1000000);
            IntegerArray = CountingSort_Integer.NewArray;
        }
    }
}
