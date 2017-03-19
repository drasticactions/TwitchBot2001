using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchPoll2001.ViewModels
{
    public class PollViewModel : NotifierBase
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                SetProperty(ref _isLoading, value);
                OnPropertyChanged();
            }
        }

        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection {
            get { return _seriesCollection; }
            set
            {
                SetProperty(ref _seriesCollection, value);
                OnPropertyChanged();
            }
        }

        private string[] _labels;
        public string[] Labels
        {
            get { return _labels; }
            set
            {
                SetProperty(ref _labels, value);
                OnPropertyChanged();
            }
        }
        public Func<double, string> Formatter { get; set; }

        public void SampleChart()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "2015",
                    Values = new ChartValues<double> { 10, 50, 39, 50 }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "2016",
                Values = new ChartValues<double> { 11, 56, 42 }
            });

            //also adding values updates and animates the chart automatically
            SeriesCollection[1].Values.Add(48d);

            Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
            Formatter = value => value.ToString("N");
        }
    }
}
