using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public static int[] SelectionSort(int[] array, SeriesCollection series)
        {
            int n = array.Length;

            for (int j = 0; j < n-1; j++)
            {
                int i = j;
                for (int k = j+1; k < n; k++)
                {
                    series[i].Values.Add((double)array[i]);
                    series[i].Values.RemoveAt(0);
                    series[j].Values.Add((double)array[j]);
                    series[j].Values.RemoveAt(0);
                    if (array[k] < array[i])
                    {
                        i = k;
                    }
                }
                if (i != j)
                {
                    int temp = array[j];
                    array[j] = array[i];
                    array[i] = temp;
                }
            }
            //int l = 0;
            //foreach (ColumnSeries j in series)
            //{
            //    j.Values.Add((double)array[l]);
            //    j.Values.RemoveAt(0);
            //    l++;
            //}
            return array;
        }

        public static int[] QuickSort(int[] array, int lo, int hi, SeriesCollection series)
        {
            if (lo < hi)
            {
                int p = _LomutoPartition(array, lo, hi, series);
                array = QuickSort(array, lo, p - 1, series);
                array = QuickSort(array, p + 1, hi, series);
            }

            return array;
        }

        private static int _LomutoPartition(int[] A, int lo, int hi, SeriesCollection series)
        {
            int pivot = A[hi];
            int i = lo - 1;
            
            // check if j is less than pivot and swap if so
            for (int j = lo; j < hi; j++)
            {
                if (A[j] < pivot)
                {
                    i = i + 1;
                    int t1 = A[i];
                    A[i] = A[j];
                    A[j] = t1;
                    series[i].Values.Add((double)A[i]);
                    series[i].Values.RemoveAt(0);
                    series[j].Values.Add((double)A[j]);
                    series[j].Values.RemoveAt(0);
                }
            }

            // increment/decrement pointers
            int t2 = A[i + 1];
            A[i + 1] = A[hi];
            A[hi] = t2;

            //int k = 0;
            //foreach (ColumnSeries j in series)
            //{
            //    j.Values.Add((double)A[k]);
            //    j.Values.RemoveAt(0);
            //    k++;
            //}

            return i + 1;
        }
    }
}
