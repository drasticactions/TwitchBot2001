﻿using System;
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

namespace TwitchPoll2001
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PollWindow _pollWindow;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenPollView_Click(object sender, RoutedEventArgs e)
        {
            if (_pollWindow == null)
            {
                _pollWindow = new PollWindow();
                _pollWindow.Show();
                _pollWindow.ViewModel.SampleChart();
            }
        }
    }
}
