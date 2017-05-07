using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using PropertyChanged;

namespace TwitchPoll2001.ViewModels
{
    [ImplementPropertyChanged]
    public class PollViewModel : NotifierBase
    {
        public PollViewModel()
        {
            AxisFontSize = 15;
            AxisColor = new SolidColorBrush(Colors.Black);
            ChartColors = new List<Color>
            {
                Colors.Red,
                Colors.Blue,
                Colors.Purple,
                Colors.Yellow,
                Colors.Green,
                Colors.Black
            };
        }
        public CartesianChart Chart { get; set; }
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

        private int _axisFontSize;
        public int AxisFontSize
        {
            get { return _axisFontSize; }
            set
            {
                SetProperty(ref _axisFontSize, value);
                OnPropertyChanged();
            }
        }

        private List<Color> _chartColors;
        public List<Color> ChartColors
        {
            get { return _chartColors; }
            set
            {
                SetProperty(ref _chartColors, value);
                OnPropertyChanged();
            }
        }

        private Brush _axisColor;
        public Brush AxisColor
        {
            get { return _axisColor; }
            set
            {
                SetProperty(ref _axisColor, value);
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

        private ObservableCollection<PollOption> _pollOptions = new ObservableCollection<PollOption>();
        public ObservableCollection<PollOption> PollOptions
        {
            get { return _pollOptions; }
            set
            {
                SetProperty(ref _pollOptions, value);
                OnPropertyChanged();
            }
        }

        private List<string> _labels;
        public List<string> Labels
        {
            get { return _labels; }
            set
            {
                SetProperty(ref _labels, value);
                OnPropertyChanged();
            }
        }

        public List<TwitchVote> TwitchVoters { get; set; } = new List<TwitchVote>();
        
        public Func<double, string> Formatter { get; set; }

        public Result AddToSet(string username, string command)
        {
            var twitchUser = TwitchVoters.FirstOrDefault(node => node.Username.Equals(username));
            if (twitchUser != null) return new Result() { IsSuccess = false, Reason = $"You've already voted for {twitchUser.Command}!" };

            var twitchCommand = PollOptions.FirstOrDefault(node => node.Command.Equals(command));
            if (twitchCommand == null) return new Result() { IsSuccess = false, Reason = $"Option {command} doesn't exist!" };

            TwitchVoters.Add(new TwitchVote { Command = command, Username = username });
            twitchCommand.Value = twitchCommand.Value + 1;
            Chart?.Update();

            return new Result() { IsSuccess = true, Reason = $"You've voted for {command}!" };
        }

        public void SetupChart()
        {
            SeriesCollection = new SeriesCollection();
            foreach (var item in PollOptions)
            {
                var test = new ChartValues<PollOption> {item};
                SeriesCollection.Add(new RowSeries()
                {
                    Values = test,
                    Title = item.Label
                });
            }
            Labels = PollOptions.Select(node => node.Label).ToList();
            Formatter = value => value.ToString();
        }
    }

    [ImplementPropertyChanged]
    public class PollOption
    {
        public int Id { get; set; }
        public string Label { get; set; }

        public string Command { get; set; }

        public double Value { get; set; }
    }

    public class Result
    {
        public bool IsSuccess { get; set; }

        public string Reason { get; set; }
    }

    public class TwitchVote
    {
        public string Username { get; set; }

        public string Command { get; set; }
    }
}
