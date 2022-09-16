using Microsoft.AspNetCore.SignalR.Client;
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
                .WithUrl("https://localhost:7169/testhub")
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
            try
            {
                await _connection.StartAsync();
                messagesList.Items.Add("Connection started");
                btnConnect.IsEnabled = false;
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }
    }
}
