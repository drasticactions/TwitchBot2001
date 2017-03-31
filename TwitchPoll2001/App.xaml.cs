using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TwitchPoll2001.ViewModels;

namespace TwitchPoll2001
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var mapper1 = Mappers.Xy<PollOption>()
                            .X((value, index) => index)
                            .Y(value => value.Value);

            var mapper2 = Mappers.Xy<PollOption>()
                .X(value => value.Value) //use the value (int) as X
                .Y((value, index) => index);

            LiveCharts.Charting.For<PollOption>(mapper1, SeriesOrientation.Horizontal);
            LiveCharts.Charting.For<PollOption>(mapper2, SeriesOrientation.Vertical);
        }
    }
}
