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
using LiveCharts.Wpf.Charts.Base;
using TwitchPoll2001.ViewModels;

namespace TwitchPoll2001
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = DataContext as MainWindowViewModel;
        }

        private void OpenPollView_Click(object sender, RoutedEventArgs e)
        {
            var _pollWindow = new PollWindow();
            _pollWindow.Show();
            _pollWindow.SetupWindow(ViewModel.PollViewModel);
            _pollWindow.ViewModel.SetupChart();
        }

        private void ResetPoll_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ResetPollButton();
        }

        private void ClearPoll_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearPollButton();
        }

        private void StartPoll_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.StartPollButton();
        }

        private void EndPoll_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.EndPollButton();
        }

        private void AxisFontColorField_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ViewModel == null || ViewModel.PollViewModel == null) return;
            var textbox = sender as TextBox;
            if (textbox == null) return;
            try
            {
                var test = (Color)System.Windows.Media.ColorConverter.ConvertFromString(textbox.Text);
                ViewModel.PollViewModel.AxisColor = new SolidColorBrush(test);
            }
            catch (Exception exception)
            {
                // Ignore errors, set it to black.
                ViewModel.PollViewModel.AxisColor = new SolidColorBrush(Colors.Black);
            }
        }

        private void AxisColors_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ViewModel == null || ViewModel.PollViewModel == null) return;
            var textbox = sender as TextBox;
            if (textbox == null) return;

            try
            {
                var colorStringList = textbox.Text.Split(',');
                ViewModel.PollViewModel.ChartColors = colorStringList.Select(item => (Color) System.Windows.Media.ColorConverter.ConvertFromString(item)).ToList();
                Chart.Colors = ViewModel.PollViewModel.ChartColors;
            }
            catch (Exception exception)
            {
                ViewModel.PollViewModel.ChartColors = new List<Color>
                {
                    Colors.Red,
                    Colors.Blue,
                    Colors.Purple,
                    Colors.Yellow,
                    Colors.Green,
                    Colors.Black
                };
                Chart.Colors = ViewModel.PollViewModel.ChartColors;
            }
        }
    }
}
