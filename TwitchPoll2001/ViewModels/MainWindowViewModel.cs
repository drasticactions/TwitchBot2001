using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib;
using TwitchLib.Events.Client;
using TwitchLib.Models.Client;

namespace TwitchPoll2001.ViewModels
{
    public class MainWindowViewModel : NotifierBase
    {
        public MainWindowViewModel()
        {
            ClickLoginButtonCommand = new AsyncDelegateCommand(async o => { await ClickLoginButton(); },
                o => CanClickLoginButton);

            AddToPollCommand = new AsyncDelegateCommand(async o => { await AddToPollButton(); },
                o => CanAddToPoll);

            JoinChannelCommand = new AsyncDelegateCommand(async o => { await JoinChannel(); },
                o => CanJoinChannel);

            LeaveChannelCommand = new AsyncDelegateCommand(async o => { await LeaveChannel(); },
                o => true);
        }

        #region Properties

        public PollViewModel PollViewModel { get; set; } = new PollViewModel();

        public bool CanClickLoginButton => !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);

        public bool CanAddToPoll => !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);

        public bool CanJoinChannel => IsLoggedIn && !string.IsNullOrEmpty(ChannelName);

        public bool CanLeaveChannel => IsLoggedIn;


        public AsyncDelegateCommand ClickLoginButtonCommand { get; private set; }

        public AsyncDelegateCommand AddToPollCommand { get; private set; }

        public AsyncDelegateCommand JoinChannelCommand { get; private set; }

        public AsyncDelegateCommand LeaveChannelCommand { get; private set; }

        public TwitchClient Client { get; set; }


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

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                SetProperty(ref _isLoggedIn, value);
                OnPropertyChanged();
            }
        }

        private bool _connectedToChannel = true;
        public bool ConnectedToChannel
        {
            get { return _connectedToChannel; }
            set
            {
                SetProperty(ref _connectedToChannel, value);
                OnPropertyChanged();
            }
        }

        private string _label;
        public string Label
        {
            get { return _label; }
            set
            {
                if (_label == value) return;
                _label = value;
                OnPropertyChanged();
                AddToPollCommand.RaiseCanExecuteChanged();
            }
        }

        private string _twitchCommand;
        public string TwitchCommand
        {
            get { return _twitchCommand; }
            set
            {
                if (_twitchCommand == value) return;
                _twitchCommand = value;
                OnPropertyChanged();
                AddToPollCommand.RaiseCanExecuteChanged();
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName == value) return;
                _userName = value;
                OnPropertyChanged();
                ClickLoginButtonCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value) return;
                _password = value;
                OnPropertyChanged();
                ClickLoginButtonCommand.RaiseCanExecuteChanged();
            }
        }

        private string _channelName;

        public string ChannelName
        {
            get { return _channelName; }
            set
            {
                if (_channelName == value) return;
                _channelName = value;
                OnPropertyChanged();
                JoinChannelCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region TwitchEvents
        private void ClientConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine("Client connected to Twitch!");
            IsLoggedIn = true;
        }


        private void ClientJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine($"Client joined channel: {e.Channel}");
            ConnectedToChannel = true;
            
        }

        private void ClientLeftChannel(object sender, OnLeftChannelArgs e)
        {
            Console.WriteLine($"Client left channel: {e.Channel}");
            ConnectedToChannel = false;
        }

        private void ClientWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            Console.WriteLine($"Client whisper received: {e.WhisperMessage}");
        }

        private void ClientWhisperCommandReceived(object sender, OnWhisperCommandReceivedArgs e)
        {
            Console.WriteLine($"Client whisper command: {e.Command} - {e.WhisperMessage.Message}");
        }

        private void ClientMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            Console.WriteLine($"Client message: {e.ChatMessage.Message}");
        }

        private void ClientChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            Console.WriteLine($"Client whisper command: {e.Command}");
            // ChatCommand
        }

        #endregion

        #region TwitchCommands

        public async Task LeaveChannel()
        {
            if (Client == null) return;
            Client.LeaveChannel(ChannelName);
        }

        public async Task JoinChannel()
        {
            if (Client == null) return;
            Client.JoinChannel(ChannelName);
        }

        #endregion

        public async Task AddToPollButton()
        {
            
        }

        public async Task ClickLoginButton()
        {
            try
            {
                Client = new TwitchClient(new ConnectionCredentials(UserName, Password), null, '!', '!', true);

                Client.OnConnected += ClientConnected;
                Client.OnJoinedChannel += ClientJoinedChannel;
                Client.OnLeftChannel += ClientLeftChannel;
                Client.OnMessageReceived += ClientMessageReceived;
                Client.OnChatCommandReceived += ClientChatCommandReceived;
                Client.OnWhisperCommandReceived += ClientWhisperCommandReceived;
                Client.OnWhisperReceived += ClientWhisperReceived;
                Client.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
