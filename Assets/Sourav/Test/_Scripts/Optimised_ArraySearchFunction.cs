using Sourav.Utilities.Scripts.Algorithms.Sorting;

namespace Sourav.Test._Scripts
{
    public class Optimised_ArraySearchFunction
    {
        public static int solution(int[] A)
        {
            CountingSort_Integer.Sort(A, -1000000, 1000000);
            A = CountingSort_Integer.NewArray;
            int small = 1;

            for (int i = 0; i < A.Length; i++)
            {
                if(A[i] == small)
                {
                    ++small;
                }
            }

            return small;
        }
    }
}
