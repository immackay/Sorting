using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;

namespace Sorting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[] array;
        private int _sortingMethod = 1;

        public MainWindow()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection { };

            array = new int[100];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 100 - i;
                SeriesCollection.Add(new ColumnSeries
                {
                    Values = new ChartValues<double> { array[i] }
                });
            }

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }

        private void Button_Click_Sort(object sender, RoutedEventArgs e)
        {
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
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, action);
                    break;
                }
            });
#endif
//#if NET45
            Task.Run(() =>
            {
                switch (_sortingMethod)
                {
                    case 0:
                        Algorithms.SelectionSort(array);
                        break;
                    case 1:
                        Algorithms.QuickSort(array, 0, array.Length - 1);
                        break;
                    default:
                        Algorithms.QuickSort(array, 0, array.Length - 1);
                        break;
                }
                while (true)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int i = 0;
                        foreach (ColumnSeries j in SeriesCollection)
                        {
                            j.Values.Add((double)array[i]);
                            j.Values.RemoveAt(0);
                            i++;
                        }
                        
                    });
                    break;
                }
            });
//#endif
        }

        private void Button_Click_QuickSort(object sender, RoutedEventArgs e)
        {
            _sortingMethod = Algorithms.List.QuickSort;
        }

        private void Button_Click_SelectionSort(object sender, RoutedEventArgs e)
        {
            _sortingMethod = Algorithms.List.SelectionSort;
        }
    }
}
