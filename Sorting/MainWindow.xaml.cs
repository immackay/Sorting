using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Sorting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MainLoop();
        }

        void MainLoop()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection { };

            int[] array = new int[101];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 100 - i;
                SeriesCollection.Add(new ColumnSeries
                {
                    Values = new ChartValues<double> { array[i] }
                });
            }


            //
            //
            // might need to iterate through and set SeriesCollection[i] to array[i]
            // then run sorting algorithm (if activated?)
            //
            //
#if NET40
            Task.Factory.StartNew(() =>
            {
 
                Action action = delegate
                {
                    int i = 0;
                    foreach (ColumnSeries j in SeriesCollection)
                    {
                        j.Values.Add((double)array[i]);
                        j.Values.RemoveAt(0);
                        i++;
                    }
                    QuickSort(array, 0, array.Length - 1);
                    // SetLecture();
                };
 
                while (true)
                {
                    Thread.Sleep(500);
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, action);
                }
            });
#endif
//#if NET45
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(500);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int i = 0;
                        foreach (ColumnSeries j in SeriesCollection)
                        {
                            j.Values.Add((double)array[i]);
                            j.Values.RemoveAt(0);
                            i++;
                        }
                        QuickSort(array, 0, array.Length - 1);
                        // SetLecture();
                    });
                }
            });
//#endif

            DataContext = this;
        }

        public int[] QuickSort(int[] array, int lo, int hi)
        {
            if (lo < hi)
            {
                int p = _partition(array, lo, hi);
                array = QuickSort(array, lo, p - 1);
                array = QuickSort(array, p + 1, hi);
            }
            
            return array;
        }

        private int _partition(int[] A, int lo, int hi)
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

        public SeriesCollection SeriesCollection { get; set; }
    }
}
