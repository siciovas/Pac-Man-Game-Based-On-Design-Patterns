using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF.Game.Views
{
    /// <summary>
    /// Interaction logic for FourthLevelView.xaml
    /// </summary>
    public partial class FifthLevelView : UserControl
    {
        public FifthLevelView()
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
        }
    }
}
