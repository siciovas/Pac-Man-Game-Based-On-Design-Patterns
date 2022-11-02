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
        }
    }
}
