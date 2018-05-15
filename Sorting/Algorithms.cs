using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    class Algorithms
    {
        public class List
        {
            internal static readonly int QuickSort = 1;
            internal static readonly int SelectionSort = 0;
        };

        public static int[] SelectionSort(int[] array)
        {
            int n = array.Length;

            for (int j = 0; j < n-1; j++)
            {
                int iMin = j;
                for (int i = j+1; i < n; i++)
                {
                    if (array[i] < array[iMin])
                    {
                        iMin = i;
                    }
                }
                if (iMin != j)
                {
                    int temp = array[j];
                    array[j] = array[iMin];
                    array[iMin] = temp;
                }
            }
            return array;
        }

        public static int[] QuickSort(int[] array, int lo, int hi)
        {
            if (lo < hi)
            {
                int p = _LomutoPartition(array, lo, hi);
                array = QuickSort(array, lo, p - 1);
                array = QuickSort(array, p + 1, hi);
            }

            return array;
        }

        private static int _LomutoPartition(int[] A, int lo, int hi)
        {
            int pivot = A[hi];
            int i = lo - 1;

            for (int j = lo; j < hi; j++)
            {
                if (A[j] < pivot)
                {
                    i = i + 1;
                    int t1 = A[i];
                    A[i] = A[j];
                    A[j] = t1;
                }
            }
            int t2 = A[i + 1];
            A[i + 1] = A[hi];
            A[hi] = t2;
            return i + 1;
        }
    }
}
