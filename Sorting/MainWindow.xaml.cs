using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        private bool _initialized = false;
        private int _sortingMethod = 1;
        private int _size = 300;

        public MainWindow()
        {
            InitializeComponent();
            
            _init();
            this.DataContext = this;
        }

        private void _init()
        {
            Random random = new Random();

            array = _InitializeArray(random);

            if (_initialized)
            {
                int i = 0;
                foreach (ColumnSeries j in SeriesCollection)
                {
                    j.Values.Add((double)array[i]);
                    j.Values.RemoveAt(0);
                    i++;
                }
            }
            else
            {
                SeriesCollection = new SeriesCollection { };
                for (int i = 0; i < array.Length - 1; i++)
                {
                    SeriesCollection.Add(new ColumnSeries
                    {
                        Values = new ChartValues<double>
                        {
                            (double)array[i]
                        }
                    });
                }
                _initialized = true;
            }
        }

        private int[] _InitializeArray(Random random)
        {
            int[] deck = new int[_size];
            int[] order = new int[_size];
            for (int i = 0; i < deck.Length; i++)
            {
                deck[i] = i + 1;
                order[i] = random.Next();
            }
            Array.Sort(order, deck);
            return deck;
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
                while (true)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        switch (_sortingMethod)
                        {
                            case 0:
                                Algorithms.SelectionSort(array, SeriesCollection);
                                break;
                            case 1:
                                Algorithms.QuickSort(array, 0, array.Length - 1, SeriesCollection);
                                break;
                            default:
                                Algorithms.QuickSort(array, 0, array.Length - 1, SeriesCollection);
                                break;
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
            Task.Run(() =>
            {
                while(true)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _init();
                    });
                    break;
                }
            });
        }

        private void Button_Click_SelectionSort(object sender, RoutedEventArgs e)
        {
            _sortingMethod = Algorithms.List.SelectionSort;
            Task.Run(() =>
            {
                while (true)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _init();
                    });
                    break;
                }
            });
        }
    }
}
