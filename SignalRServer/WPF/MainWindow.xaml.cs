using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection _connection;
        public MainWindow()
        {
            InitializeComponent();
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7169/serverhub")
                .Build();
            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };
              
        }

        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            _connection.On<string>("Connected",
                (connectionid) =>
            {
                tbMain.Text = connectionid;
            });
            _connection.On<string>("Posted", (value) =>
            {
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    messagesList.Items.Add(value);
                }));
            });
            _connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    AddToList(user + " says " + message);
                }));
            });
            try
            {
                await _connection.StartAsync();
                messagesList.Items.Add("Connection started");
                btnConnect.Visibility = Visibility.Collapsed;
                var SendPanel = FindName("sendPanel") as StackPanel;
                SendPanel.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }

        private async void BtnSend_Send(object sender, RoutedEventArgs e)
        {
            var user = FindName("userName") as TextBox;
            var message = FindName("message") as TextBox;
          
            if(user != null && message != null)
            {
                await _connection.InvokeAsync("SendMessage", user.Text, message.Text);
            }
        }

        private void AddToList(string value)
        {
            messagesList.Items.Add(value);
        }
    }
}
