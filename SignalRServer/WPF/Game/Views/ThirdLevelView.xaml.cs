using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF.Game.Views
{
    /// <summary>
    /// Interaction logic for ThirdLevelView.xaml
    /// </summary>
    public partial class ThirdLevelView : UserControl
    {
        public ThirdLevelView()
        {
            InitializeComponent();
            GameSetup();
        }

        private void GameSetup()
        {
            MyCanvas.Focus();
        }
    }
}
