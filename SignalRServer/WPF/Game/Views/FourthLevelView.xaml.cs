using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF.Game.Views
{
    /// <summary>
    /// Interaction logic for FourthLevelView.xaml
    /// </summary>
    public partial class FourthLevelView : UserControl
    {
        public FourthLevelView()
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
