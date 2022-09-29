using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Linq;
using WPF.Game.Factory.Interfaces;
using WPF.Game.Factory.Classes;
using System.Collections.Generic;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection _connection;
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool goLeft, goRight, goUp, goDown;
        bool noLeft, noRight, noUp, noDown;
        string oponentConnectionId;
        int speed = 8;
        CoinFactory _coinFactory;
        List<ICoin> allCoins = new List<ICoin>();

        Rect pacmanHitBox;

        int ghostSpeed = 10;
        int ghostMoveStep = 130;
        int currentGhostStep;
        int score = 0;
        int oponentScore = 0;

        public MainWindow()
        {
            _coinFactory = new CoinFactory();
            InitializeComponent();
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7169/serverhub")
                .Build();
            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };

            GameSetup();
            ListenServer();
        }

        private void ListenServer()
        {
            _connection.On<string, string>("ReceiveMessage", (connectionId, move) =>
            {
                Dispatcher.BeginInvoke((Action)(() => {
                    oponentConnectionId = connectionId;
                    if (oponentConnectionId != _connection.ConnectionId)
                    {
                        if (move == "left" && !noLeft)
                        {
                            goRight = goUp = goDown = false;
                            noRight = noUp = noDown = false;

                            goLeft = true;

                            oponentPacman.RenderTransform = new RotateTransform(-180, oponentPacman.Width / 2, oponentPacman.Height / 2);
                        }
                        if (move == "right" && !noRight)
                        {
                            noLeft = noUp = noDown = false;
                            goLeft = goUp = goDown = false;

                            goRight = true;

                            oponentPacman.RenderTransform = new RotateTransform(0, oponentPacman.Width / 2, oponentPacman.Height / 2);

                        }

                        if (move == "up" && !noUp)
                        {
                            noRight = noDown = noLeft = false;
                            goRight = goDown = goLeft = false;

                            goUp = true;

                            oponentPacman.RenderTransform = new RotateTransform(-90, oponentPacman.Width / 2, oponentPacman.Height / 2);
                        }

                        if (move == "down" && !noDown)
                        {
                            noUp = noLeft = noRight = false;
                            goUp = goLeft = goRight = false;

                            goDown = true;

                            oponentPacman.RenderTransform = new RotateTransform(90, oponentPacman.Width / 2, oponentPacman.Height / 2);
                        }
                    }
                }));
            });
        }

        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
           /* _connection.On<string>("Connected",
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
            }*/
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

       /* private void AddToList(string value)
        {
            messagesList.Items.Add(value);
        }*/

        private async void CanvasKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Left && !noLeft)
            {
                goRight = goUp = goDown = false;
                noRight = noUp = noDown = false; 

                goLeft = true;

                pacman.RenderTransform = new RotateTransform(-180, pacman.Width / 2, pacman.Height / 2);
                await _connection.InvokeAsync("SendMessage", _connection.ConnectionId, "left");
            }

            if (e.Key == Key.Right && !noRight)
            {
                noLeft = noUp = noDown = false;
                goLeft = goUp = goDown = false; 

                goRight = true; 

                pacman.RenderTransform = new RotateTransform(0, pacman.Width / 2, pacman.Height / 2);
                await _connection.InvokeAsync("SendMessage", _connection.ConnectionId, "right");

            }

            if (e.Key == Key.Up && !noUp)
            {
                noRight = noDown = noLeft = false; 
                goRight = goDown = goLeft = false; 

                goUp = true; 

                pacman.RenderTransform = new RotateTransform(-90, pacman.Width / 2, pacman.Height / 2);
                await _connection.InvokeAsync("SendMessage", _connection.ConnectionId, "up");
            }

            if (e.Key == Key.Down && !noDown)
            {
                noUp = noLeft = noRight = false; 
                goUp = goLeft = goRight = false; 

                goDown = true; 

                pacman.RenderTransform = new RotateTransform(90, pacman.Width / 2, pacman.Height / 2);
                await _connection.InvokeAsync("SendMessage", _connection.ConnectionId, "down");
            }
        }

        private async void GameSetup()
        {
            MyCanvas.Focus();
            gameTimer.Tick += GameLoop;
            gameTimer.Tick += OponentControll;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20); ///will tick every 20ms
            gameTimer.Start();
            currentGhostStep = ghostMoveStep;

            //add images for pacman
            ImageBrush pacmanBrush = new ImageBrush();
            pacmanBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacman.jpg"));
            pacman.Fill = pacmanBrush;
            ImageBrush oponentPacmanBrush = new ImageBrush();
            oponentPacmanBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacmanOp.jpg"));
            oponentPacman.Fill = oponentPacmanBrush;

            //add images to ghosts too 
            ImageBrush redGhost = new ImageBrush();
            redGhost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/red.jpg"));
            redguy.Fill = redGhost;
            ImageBrush orangeGhost = new ImageBrush();
            orangeGhost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/orange.jpg"));
            orangeguy.Fill = orangeGhost;
            ImageBrush pinkGhost = new ImageBrush();
            pinkGhost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink.jpg"));
            pinkguy.Fill = pinkGhost;

            FindAllCoins();

            try
            {
                await _connection.StartAsync();
               /* messagesList.Items.Add("Connection started");
                btnConnect.Visibility = Visibility.Collapsed;*/
                /*var SendPanel = FindName("sendPanel") as StackPanel;
                SendPanel.Visibility = Visibility.Visible;*/
            }
            catch (Exception ex)
            {
                
            }
        }

        private void FindAllCoins()
        {
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "coin")
                {
                    ICoin coin = _coinFactory.GetCoin(1);
                    allCoins.Add(coin);
                }
            }
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            txtScore.Content = "Score: " + score;
            // show the scoreo to the txtscore label. 
            if (oponentConnectionId == _connection.ConnectionId)
            {
                if (goRight)
                {
                    Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + speed);
                }
                if (goLeft)
                {
                    Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - speed);
                }
                if (goUp)
                {
                    Canvas.SetTop(pacman, Canvas.GetTop(pacman) - speed);
                }
                if (goDown)
                {
                    Canvas.SetTop(pacman, Canvas.GetTop(pacman) + speed);
                }

                if (goDown && Canvas.GetTop(pacman) + 280 > Application.Current.MainWindow.Height)
                {
                    noDown = true;
                    goDown = false;
                }
                if (goUp && Canvas.GetTop(pacman) < 5)
                {
                    noUp = true;
                    goUp = false;
                }
                if (goLeft && Canvas.GetLeft(pacman) - 10 < 1)
                {
                    noLeft = true;
                    goLeft = false;
                }
                if (goRight && Canvas.GetLeft(pacman) + 70 > Application.Current.MainWindow.Width)
                {
                    noRight = true;
                    goRight = false;
                }

                pacmanHitBox = new Rect(Canvas.GetLeft(pacman), Canvas.GetTop(pacman), pacman.Width, pacman.Height);

                foreach (var x in MyCanvas.Children.OfType<Rectangle>())
                {
                    // loop through all of the rectangles inside of the game and identify them using the x variable

                    Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // create a new rect called hit box for all of the available rectangles inside of the game

                    // find the walls, if any of the rectangles inside of the game has the tag wall inside of it
                    if ((string)x.Tag == "wall")
                    {
                        // check if we are colliding with the wall while moving left if true then stop the pac man movement
                        if (goLeft == true && pacmanHitBox.IntersectsWith(hitBox))
                        {
                            Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + 10);
                            noLeft = true;
                            goLeft = false;
                        }
                        // check if we are colliding with the wall while moving right if true then stop the pac man movement
                        if (goRight == true && pacmanHitBox.IntersectsWith(hitBox))
                        {
                            Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - 10);
                            noRight = true;
                            goRight = false;
                        }
                        // check if we are colliding with the wall while moving down if true then stop the pac man movement
                        if (goDown == true && pacmanHitBox.IntersectsWith(hitBox))
                        {
                            Canvas.SetTop(pacman, Canvas.GetTop(pacman) - 10);
                            noDown = true;
                            goDown = false;
                        }
                        // check if we are colliding with the wall while moving up if true then stop the pac man movement
                        if (goUp == true && pacmanHitBox.IntersectsWith(hitBox))
                        {
                            Canvas.SetTop(pacman, Canvas.GetTop(pacman) + 10);
                            noUp = true;
                            goUp = false;
                        }
                    }

                    // check if the any of the rectangles has a coin tag inside of them
                    if ((string)x.Tag == "coin")
                    {
                        // if pac man collides with any of the coin and coin is still visible to the screen
                        if (pacmanHitBox.IntersectsWith(hitBox) && x.Visibility == Visibility.Visible)
                        {
                            // set the coin visiblity to hidden
                            x.Visibility = Visibility.Hidden;
                            // add 1 to the score
                            score = score + allCoins.First().Value;
                            allCoins.RemoveAt(0);
                        }
                    }
                }
            }
        }

        private void OponentControll(object? sender, EventArgs e)
        {
            oponentTxtScore.Content = "Oponent Score: " + oponentScore;
            if (oponentConnectionId != _connection.ConnectionId)
            {
                if (goRight)
                {
                    Canvas.SetLeft(oponentPacman, Canvas.GetLeft(oponentPacman) + speed);
                }
                if (goLeft)
                {
                    Canvas.SetLeft(oponentPacman, Canvas.GetLeft(oponentPacman) - speed);
                }
                if (goUp)
                {
                    Canvas.SetTop(oponentPacman, Canvas.GetTop(oponentPacman) - speed);
                }
                if (goDown)
                {
                    Canvas.SetTop(oponentPacman, Canvas.GetTop(oponentPacman) + speed);
                }

                if (goDown && Canvas.GetTop(oponentPacman) + 280 > Application.Current.MainWindow.Height)
                {
                    noDown = true;
                    goDown = false;
                }
                if (goUp && Canvas.GetTop(oponentPacman) < 5)
                {
                    noUp = true;
                    goUp = false;
                }
                if (goLeft && Canvas.GetLeft(oponentPacman) - 10 < 1)
                {
                    noLeft = true;
                    goLeft = false;
                }
                if (goRight && Canvas.GetLeft(oponentPacman) + 70 > Application.Current.MainWindow.Width)
                {
                    noRight = true;
                    goRight = false;
                }

                pacmanHitBox = new Rect(Canvas.GetLeft(oponentPacman), Canvas.GetTop(oponentPacman), oponentPacman.Width, oponentPacman.Height);

                foreach (var x in MyCanvas.Children.OfType<Rectangle>())
                {
                    // loop through all of the rectangles inside of the game and identify them using the x variable

                    Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // create a new rect called hit box for all of the available rectangles inside of the game

                    // find the walls, if any of the rectangles inside of the game has the tag wall inside of it
                    if ((string)x.Tag == "wall")
                    {
                        // check if we are colliding with the wall while moving left if true then stop the pac man movement
                        if (goLeft == true && pacmanHitBox.IntersectsWith(hitBox))
                        {
                            Canvas.SetLeft(oponentPacman, Canvas.GetLeft(oponentPacman) + 10);
                            noLeft = true;
                            goLeft = false;
                        }
                        // check if we are colliding with the wall while moving right if true then stop the pac man movement
                        if (goRight == true && pacmanHitBox.IntersectsWith(hitBox))
                        {
                            Canvas.SetLeft(oponentPacman, Canvas.GetLeft(oponentPacman) - 10);
                            noRight = true;
                            goRight = false;
                        }
                        // check if we are colliding with the wall while moving down if true then stop the pac man movement
                        if (goDown == true && pacmanHitBox.IntersectsWith(hitBox))
                        {
                            Canvas.SetTop(oponentPacman, Canvas.GetTop(oponentPacman) - 10);
                            noDown = true;
                            goDown = false;
                        }
                        // check if we are colliding with the wall while moving up if true then stop the pac man movement
                        if (goUp == true && pacmanHitBox.IntersectsWith(hitBox))
                        {
                            Canvas.SetTop(oponentPacman, Canvas.GetTop(oponentPacman) + 10);
                            noUp = true;
                            goUp = false;
                        }
                    }

                    // check if the any of the rectangles has a coin tag inside of them
                    if ((string)x.Tag == "coin")
                    {
                        // if pac man collides with any of the coin and coin is still visible to the screen
                        if (pacmanHitBox.IntersectsWith(hitBox) && x.Visibility == Visibility.Visible)
                        {
                            // set the coin visiblity to hidden
                            x.Visibility = Visibility.Hidden;
                            // add 1 to the score
                            oponentScore++;
                        }
                    }
                }
            }
        }

        private void GameOver(string message)
        {
            gameTimer.Stop();
            MessageBox.Show(message, "PacMan Multiplayer");
            
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location); //start a new game
            Application.Current.Shutdown(); //shtdown current game 
        }
    }
}
