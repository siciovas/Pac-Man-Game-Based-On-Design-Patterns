using System.Windows.Media;

namespace ClassLibrary.Mobs.Interfaces
{
    public interface IZombie
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public ImageBrush Fill { get; set; }
    }
}
