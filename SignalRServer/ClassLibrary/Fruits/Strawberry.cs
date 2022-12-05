using ClassLibrary.MainUnit;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Fruits
{
    public class Strawberry : Unit
    {
        public Strawberry(int top, int left)
        {
            UnitType = new UnitType();

            Top = top;
            Left = left;
            ImageBrush strawberry = new ImageBrush();
            strawberry.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/strawberry.png"));
            UnitType.Appearance = strawberry;
            UnitType.Name = "Strawberry";
        }

        public override Strawberry Copy()
        {
            return (Strawberry)this.MemberwiseClone();
        }
    }
}
