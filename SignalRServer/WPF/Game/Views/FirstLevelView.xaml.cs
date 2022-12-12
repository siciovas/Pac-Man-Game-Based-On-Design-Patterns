using System;
using System.Windows;
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

        }
    }
}
