namespace Sourav.Utilities.Scripts.Algorithms.Sorting
{
    public class CountingSort_Integer
    {
        public static int[] NewArray;

        public static void Sort(int[] a, int min, int max)
        {
            int[] count = new int[(max - min)];
            NewArray = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                count[a[i]]++;
            }

            int index = -1;
            for (int i = 0; i < count.Length; i++)
            {
                int counter = count[i];
                for (int j = 0; j < counter; j++)
                {
                    NewArray[++index] = i;
                }
            }
        }
    }
}
