using GalaSoft.MvvmLight.Command;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;
using WPF.Connection;
using WPF.Game.Factory.Classes;
using WPF.Game.Factory.Interfaces;
using WPF.Game.Singleton.Classes;
using WPF.Views;

namespace WPF.Levels
{
    internal class FirstLevelViewModel : ViewModelBase
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool goLeft, goRight, goUp, goDown;
        bool noLeft, noRight, noUp, noDown;
        string oponentConnectionId;
        int speed = 8;
        CoinFactory _coinFactory;
        HubConnection _connection;
        IConnectionProvider _connectionProvider;
        List<ICoin> allCoins = new List<ICoin>();
        private int _yellowPacmanLeft;
        public int YellowPacmanLeft
        {
            get
            {
                return _yellowPacmanLeft;
            }
            private set
            {
                if (value != _yellowPacmanLeft)
                {
                    _yellowPacmanLeft = value;
                    OnPropertyChanged("YellowPacmanLeft");
                }
            }
        }

        private int _yellowPacmanTop;
        public int YellowPacmanTop
        {
            get
            {
                return _yellowPacmanTop;
            }
            private set
            {
                if (value != _yellowPacmanTop)
                {
                    _yellowPacmanTop = value;
                    OnPropertyChanged("YellowPacmanTop");
                }
            }
        }

        private int _greenPacmanLeft;

        public int GreenPacmanLeft
        {
            get
            {
                return _greenPacmanLeft;
            }
            private set
            {
                if (value != _greenPacmanLeft)
                {
                    _greenPacmanLeft = value;
                    OnPropertyChanged("GreenPacmanLeft");
                }
            }
        }

        private int _greenPacmanTop;
        public int GreenPacmanTop
        {
            get
            {
                return _greenPacmanTop;
            }
            private set
            {
                if (value != _greenPacmanTop)
                {
                    _greenPacmanTop = value;
                    OnPropertyChanged("GreenPacmanTop");
                }
            }
        }

        PacmanHitbox myPacmanHitBox = PacmanHitbox.GetInstance;

        int ghostSpeed = 10;
        int ghostMoveStep = 130;
        int currentGhostStep;
        int score = 0;
        int oponentScore = 0;

        public FirstLevelViewModel(IConnectionProvider connectionProvider)
        {
            _coinFactory = new CoinFactory();
            _connection = connectionProvider.GetConnection();

            GreenPacmanTop = 20;
            GreenPacmanLeft = 20;
            YellowPacmanLeft = 20;
            YellowPacmanTop = 20;

            GameSetup();
            ListenServer();
        }


        private void ListenServer()
        {
            _connection.On<int, int>("OponentCordinates", (top, left) =>
            {
                GreenPacmanLeft = left;
                GreenPacmanTop = top;
            });
        }

        private async void GameSetup()
        {
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(30); ///will tick every 20ms
            gameTimer.Start();
            currentGhostStep = ghostMoveStep;

           // FindAllCoins();
        }

        private void FindAllCoins()
        {
            /*foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "coin")
                {
                    ICoin coin = _coinFactory.GetCoin(1);
                    allCoins.Add(coin);
                }
            }*/
        }

        private async void GameLoop(object? sender, EventArgs e)
        {
            //txtScore.Content = "Score: " + score; TODO bind to score property 
            // show the scoreo to the txtscore label. 

            int AppHeight = (int)Application.Current.MainWindow.Height;
            int AppWidth = (int)Application.Current.MainWindow.Width;
            int oldLeft = YellowPacmanLeft;
            int oldTop = YellowPacmanTop;
            if (goRight)
            {
                YellowPacmanLeft+= speed;
            }
            if (goLeft)
            {
                YellowPacmanLeft -= speed; 
            }
            if (goUp)
            {
                YellowPacmanTop -= speed;
            }
            if (goDown)
            {
                YellowPacmanTop += speed;
            }

            if (oldLeft != YellowPacmanLeft || oldTop != YellowPacmanTop)
            {
                await _connection.InvokeAsync("SendPacManCordinates", YellowPacmanTop, YellowPacmanLeft);
            }

            if (goDown && YellowPacmanTop + 280 > AppHeight)
            {
                noDown = true;
                goDown = false;
            }
            if (goUp && YellowPacmanTop < 5)
            {
                noUp = true;
                goUp = false;
            }
            if (goLeft && YellowPacmanLeft - 5 < 1)
            {
                noLeft = true;
                goLeft = false;
            }
            if (goRight && YellowPacmanLeft + 40 > AppWidth)
            {
                noRight = true;
                goRight = false;
            }

            Rect pacmanHitBox = myPacmanHitBox.GetCurrentHitboxPosition(YellowPacmanLeft, YellowPacmanTop, 30, 30);

            /*foreach (var x in MyCanvas.Children.OfType<Rectangle>())
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
                }*/

        }

        public override void OnRightClick()
        {
            if (!noRight)
            {
                noLeft = noUp = noDown = false;
                goLeft = goUp = goDown = false;

                goRight = true;

               // pacman.RenderTransform = new RotateTransform(0, pacman.Width / 2, pacman.Height / 2);
               // _connection.InvokeAsync("SendMessage", _connection.ConnectionId, "right");

            }
        }

        public override void OnDownClick()
        {
            if (!noDown)
            {
                noUp = noLeft = noRight = false;
                goUp = goLeft = goRight = false;

                goDown = true;

               // pacman.RenderTransform = new RotateTransform(90, pacman.Width / 2, pacman.Height / 2);
                //_connection.InvokeAsync("SendMessage", _connection.ConnectionId, "down");
            }
           
        }

        public override void OnUpClick()
        {
            if (!noUp)
            {
                noRight = noDown = noLeft = false;
                goRight = goDown = goLeft = false;

                goUp = true;

               // pacman.RenderTransform = new RotateTransform(-90, pacman.Width / 2, pacman.Height / 2);
               // _connection.InvokeAsync("SendMessage", _connection.ConnectionId, "up");
            }
        }

        public override void OnLeftClick()
        {
            if (!noLeft)
            {
                goRight = goUp = goDown = false;
                noRight = noUp = noDown = false;

                goLeft = true;

               /* pacman.RenderTransform = new RotateTransform(-180, pacman.Width / 2, pacman.Height / 2);*/
               // _connection.InvokeAsync("SendMessage", _connection.ConnectionId, "left");
            }
        }
    }

        /*private void OponentControll(object? sender, EventArgs e)
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

                Rect pacmanHitBox = myPacmanHitBox.GetCurrentHitboxPosition(Canvas.GetLeft(oponentPacman), Canvas.GetTop(oponentPacman), oponentPacman.Width, oponentPacman.Height);

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
        }*/

       /* private void GameOver(string message)
        {
            gameTimer.Stop();
            MessageBox.Show(message, "PacMan Multiplayer");

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location); //start a new game
            Application.Current.Shutdown(); //shtdown current game 
        }*/
    }

