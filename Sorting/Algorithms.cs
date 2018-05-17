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
            internal static readonly int SelectionSort = 0;
            internal static readonly int QuickSort = 1;
            internal static readonly int InsertionSort = 2;
            internal static readonly int RadixSort = 3;
        };

        public static void SelectionSort(int[] array, SeriesCollection series)
        {
            int n = array.Length;

            for (int j = 0; j < n-1; j++)
            {
                int i = j;
                for (int k = j+1; k < n; k++)
                {
                    _SeriesSwap(series, i, j, array);
                    //int m = 0;
                    //foreach (ColumnSeries o in series)
                    //{
                    //    o.Values.Add((double)array[m]);
                    //    o.Values.RemoveAt(0);
                    //    m++;
                    //}
                    if (array[k] < array[i])
                    {
                        i = k;
                    }
                }
                if (i != j)
                {
                    _ArraySwap(array, i, j);
                }
            }
        }

        public static void QuickSort(int[] array, int lo, int hi, SeriesCollection series)
        {
            int _LomutoPartition(int[] A)
            {
                int pivot = A[hi];
                int i = lo - 1;
                for (int j = lo; j < hi; j++)
                {
                    if (A[j] < pivot)
                    {
                        i++;
                        _ArraySwap(A, i, j);
                        Task.Delay(TimeSpan.FromMilliseconds(100));
                        _SeriesSwap(series, i, j, A);
                    }
                }
                int t2 = A[i + 1];
                A[i + 1] = A[hi];
                A[hi] = t2;

                return i + 1;
            }
            if (lo < hi)
            {
                int p = _LomutoPartition(array);
                QuickSort(array, lo, p - 1, series);
                QuickSort(array, p + 1, hi, series);
            }
        }
        

        public static void InsertionSort(int[] array, SeriesCollection series)
        {
            for (int i = 1; i < array.Length - 1; i++)
            {
                int x = array[i];
                int j = i - 1;
                while (j >= 0 && array[j] > x)
                {
                    array[j + 1] = array[j];
                    series[j + 1].Values.Add((double)array[j + 1]);
                    series[j + 1].Values.RemoveAt(0);
                    j--;
                }
                array[j + 1] = x;
                series[j + 1].Values.Add((double)array[j + 1]);
                series[j + 1].Values.RemoveAt(0);
            }
        }

        public static void RadixSort(int[] array, int b, SeriesCollection series)
        {
            List<int>[] list_to_buckets(int[] A, int Base, int iteration) 
            {
                List<int>[] buckets = new List<int>[Base];
                for (int k = 0; k < Base; k++)
                {
                    buckets[k] = new List<int>();
                }
                foreach (int i in A)
                {
                    int digit = (int)(i / Math.Pow(Base, iteration) % Base);
                    buckets[digit].Add(i);
                };
                return buckets;
            }

            List<int> buckets_to_list(List<int>[] buckets)
            {
                List<int> numbers = new List<int>();
                foreach (List<int> bucket in buckets)
                {
                    foreach (int i in bucket)
                    {
                        numbers.Add(i);
                    }
                }
                return numbers;
            }

            int maxval = array.Max();

            int j = 0;

            while (Math.Pow(b, j) <= maxval)
            {
                array = buckets_to_list(list_to_buckets(array, b, j)).ToArray();
                int i = 0;
                foreach (ColumnSeries k in series)
                {
                    k.Values.Add((double)array[i]);
                    k.Values.RemoveAt(0);
                    i++;
                }
                j++;
            }
        }

        private static void _ArraySwap(int[] array, int x, int y)
        {
            int temp = array[x];
            array[x] = array[y];
            array[y] = temp;
        }

        private static void _SeriesSwap(SeriesCollection series, int x, int y, int[] array)
        {
            series[x].Values.Add((double)array[x]);
            series[x].Values.RemoveAt(0);
            series[y].Values.Add((double)array[y]);
            series[y].Values.RemoveAt(0);
        }
    }
}
