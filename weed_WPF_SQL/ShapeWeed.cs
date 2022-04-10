using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace weed_WPF_SQL
{
    static class ShapeWeed
    {
        public static Rectangle DrawFlowerPot(int width, int height, Cultivator c, double left, double top)
        {
            Rectangle newRectangle = new Rectangle();

            newRectangle.Width = width;
            newRectangle.Height = height;

            if (c.CyclesPassed != 11)
            {
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
            }
            else
            {
                newRectangle.Fill = Brushes.Transparent;
            }


            newRectangle.Stroke = Brushes.Black;
            newRectangle.StrokeThickness = 1;

            Canvas.SetLeft(newRectangle, left);
            Canvas.SetTop(newRectangle, top);

            return newRectangle;
        }

        public static Rectangle DrawTable(int width, int height, double left, double top)
        {
            Rectangle newRectangle = new Rectangle();

            newRectangle.Width = width;
            newRectangle.Height = height;
            newRectangle.Fill = Brushes.Gray;

            Canvas.SetLeft(newRectangle, left);
            Canvas.SetTop(newRectangle, top);

            return newRectangle;
        }

        public static Polygon DrawLamp(double left, double top, Cultivator c)
        {
            Polygon newLamp = new Polygon();
            PointCollection myPointCollection = new PointCollection();

            if (c.CyclesPassed != 11)
            {
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
            }
            else
            {
                newLamp.Fill = Brushes.Transparent;
            }

            myPointCollection.Add(new System.Windows.Point(100, 50));
            myPointCollection.Add(new System.Windows.Point(50, 100));
            myPointCollection.Add(new System.Windows.Point(150, 100));
            myPointCollection.Add(new System.Windows.Point(100, 50));
            myPointCollection.Add(new System.Windows.Point(100, 30));
            newLamp.Stroke = Brushes.Black;
            newLamp.Points = myPointCollection;

            Canvas.SetLeft(newLamp, left);
            Canvas.SetTop(newLamp, top);

            return newLamp;
        }
    }
}
