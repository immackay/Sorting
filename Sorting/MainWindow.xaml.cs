using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using LiveCharts;
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

            int[] array = new int[101];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 100 - i;
            }

            SeriesCollection = new SeriesCollection { };

            for (int i = 0; i < array.Length; i++)
            {
                SeriesCollection.Add(new ColumnSeries
                {
                    Values = new ChartValues<double> { array[i] }
                });
            }

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
    }

    public class Algorithms
    {
        public int[] QuickSort(int[] array)
        {
            return array;
        }
    }
}
