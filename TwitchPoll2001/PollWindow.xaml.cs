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
using System.Windows.Shapes;
using TwitchPoll2001.ViewModels;

namespace TwitchPoll2001
{
    /// <summary>
    /// Interaction logic for PollWindow.xaml
    /// </summary>
    public partial class PollWindow : Window
    {
        public PollViewModel ViewModel { get; set; }
        public PollWindow()
        {
            InitializeComponent();
        }

        public void SetupWindow(PollViewModel vm)
        {
            DataContext = vm;
            ViewModel = DataContext as PollViewModel;
            ViewModel.Chart = NiceChart;
        }
    }
}
