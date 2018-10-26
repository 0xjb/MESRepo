using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;

namespace Wpf.CartesianChart.Basic_Bars
{
    partial class BasicColumn : UserControl
    {
        public BasicColumn()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Ingredients",
                    Values = new ChartValues<double> { 10, 50, 39, 50, 100 }
                }
            };

            //adding series will update and animate the chart automatically
            //SeriesCollection.Add(new ColumnSeries {
            //    Title = "2016",
            //    Values = new ChartValues<double> { 11, 56, 42, 99 }
            //});
            //also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            Labels = new[] { "Barley", "Hops", "Malt", "Wheat", "Yeast" };
            Formatter = value => value.ToString("N");
            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}
