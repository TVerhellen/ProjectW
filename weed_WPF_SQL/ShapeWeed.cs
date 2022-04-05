using System.Windows.Media;
using System.Windows.Shapes;

namespace weed_WPF_SQL
{
    static class ShapeWeed
    {
        public static Rectangle DrawRectangle(int width, int height, Cultivator c)
        {
            Rectangle newRectangle = new Rectangle();

            newRectangle.Width = width;
            newRectangle.Height = height;

            switch (c.CultID)
            {
                case 1:
                    newRectangle.Fill = Brushes.GreenYellow;
                    break;
                case 2:
                    newRectangle.Fill = Brushes.BlueViolet;
                    break;
                case 3:
                    newRectangle.Fill = Brushes.Purple;
                    break;
                default:
                    break;
            }

            newRectangle.Stroke = Brushes.Black;
            newRectangle.StrokeThickness = 1;

            return newRectangle;
        }

        public static Rectangle DrawRectangleTable(int width, int height)
        {
            Rectangle newRectangle = new Rectangle();

            newRectangle.Width = width;
            newRectangle.Height = height;
            newRectangle.Fill = Brushes.Gray;

            return newRectangle;
        }

        public static Polygon DrawLamp(Cultivator c)
        {
            Polygon newLamp = new Polygon();
            PointCollection myPointCollection = new PointCollection();


            switch (c.LampID)
            {
                case 1:
                    newLamp.Fill = Brushes.Yellow;
                    break;
                case 2:
                    newLamp.Fill = Brushes.Violet;
                    break;
                case 4:
                    newLamp.Fill = Brushes.AliceBlue;
                    break;
                case 5:
                    newLamp.Fill = Brushes.Cyan;
                    break;
                default:
                    break;
            }
            myPointCollection.Add(new System.Windows.Point(100, 50));
            myPointCollection.Add(new System.Windows.Point(50, 100));
            myPointCollection.Add(new System.Windows.Point(150, 100));
            myPointCollection.Add(new System.Windows.Point(100, 300));
            newLamp.Stroke = Brushes.Black;
            newLamp.Points = myPointCollection;


            switch (c.CultID)
            {
                case 1:
                    newRectangle.Fill = Brushes.GreenYellow;
                    break;
                case 2:
                    newRectangle.Fill = Brushes.BlueViolet;
                    break;
                case 3:
                    newRectangle.Fill = Brushes.Purple;
                    break;
                default:
                    break;
            }

            newRectangle.Stroke = Brushes.Black;
            newRectangle.StrokeThickness = 1;

            return newRectangle;
        }
    }
}
