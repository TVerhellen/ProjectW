using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace weed_WPF_SQL
{
    internal class Building
    {
        public Rectangle Figure = new Rectangle();
        private int[] _lefttop;
        private int _height;
        private int _width;
        private bool _hasCollision;

        public Building(int[] lefttop, int width, int height, bool collision)
        {
            LeftTop = lefttop;
            Height = height;
            Width = width;
            HasCollision = collision;
            Figure.Fill = Brushes.DimGray;
            Figure.Width = Width;
            Figure.Height = Height;
        }

        public int[] LeftTop { get { return _lefttop; } set { if (value.Length == 2) { _lefttop = value; } } }
        public int Height { get { return _height; } set { _height = value; } }
        public int Width { get { return _width; } set { _width = value; } }
        public bool HasCollision { get { return _hasCollision; } set { _hasCollision = value; } }
        
    }
}
