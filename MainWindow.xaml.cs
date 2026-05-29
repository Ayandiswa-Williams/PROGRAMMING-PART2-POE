
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace CybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        private readonly ChatBot _chatBot;
        private readonly AudioPlayer _audioPlayer;

        public MainWindow()
        {
            InitializeComponent();

            // Startup features from Part 1
            _audioPlayer = new AudioPlayer();
            _audioPlayer.PlayGreeting();

            LoadAsciiArt();

            _chatBot = new ChatBot();
            AppendBotMessage(_chatBot.GetGreeting());
        }

        private void LoadAsciiArt()
        {
            string ascii = @"
   _____      _                 ____        _ 
  / ____|    | |               |  _ \      | |  
 | |    _   _| |__   ___ _ __  | |_) | ___ | |_ 
 | |   | | | | '_ \ / _ \ '__| |  _ < / _ \| __|
 | |___| |_| | |_) |  __/ |    | |_) | (_) | |_ 
  \_____\__, |_.__/ \___|_|    |____/ \___/ \__|
         __/ |                                  
        |___/                                    ";
            AsciiArt.Text = ascii;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            string input = UserInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            AppendUserMessage(input);
            UserInput.Clear();

            string response = _chatBot.ProcessInput(input);
            AppendBotMessage(response);

            // Auto-scroll
            ChatScroll.ScrollToEnd();
        }

        private void AppendUserMessage(string message)
        {
            ChatDisplay.Inlines.Add(new Run($"You: {message}\n") { Foreground = new SolidColorBrush(Colors.Yellow) });
        }

        private void AppendBotMessage(string message)
        {
            ChatDisplay.Inlines.Add(new Run($"Bot: {message}\n") { Foreground = new SolidColorBrush(Colors.Cyan) });
        }
    }
}