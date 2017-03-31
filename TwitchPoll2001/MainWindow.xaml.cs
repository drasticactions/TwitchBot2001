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
    }
}
