using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF.Game.Views
{
    /// <summary>
    /// Interaction logic for FirstLevelView.xaml
    /// </summary>
    public partial class FirstLevelView : UserControl
    {
        public FirstLevelView()
        {
            InitializeComponent();
            GameSetup();
        }

        private void GameSetup()
        {
            MyCanvas.Focus();

            //add images for pacman
            ImageBrush pacmanBrush = new ImageBrush();
            pacmanBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacman.jpg"));
            pacman.Fill = pacmanBrush;
            ImageBrush oponentPacmanBrush = new ImageBrush();
            oponentPacmanBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacmanOp.jpg"));
            oponentPacman.Fill = oponentPacmanBrush;

            //add images to ghosts too 
         /*   ImageBrush redGhost = new ImageBrush();
            redGhost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/red.jpg"));
            redguy.Fill = redGhost;
            ImageBrush orangeGhost = new ImageBrush();
            orangeGhost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/orange.jpg"));
            orangeguy.Fill = orangeGhost;
            ImageBrush pinkGhost = new ImageBrush();
            pinkGhost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink.jpg"));
            pinkguy.Fill = pinkGhost;*/

        }
    }
}
