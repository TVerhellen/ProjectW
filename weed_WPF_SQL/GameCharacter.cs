using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace weed_WPF_SQL
{
    internal abstract class GameCharacter
    {
        private int[] _location;
        private int _speed;
        internal List<int[]> _routeCoordinates; //2 points, index 0 is furthest in positive x or y
        internal int[] _currentTarget;
        internal int direction;
        public Brush Fill;
        public Rectangle Figure = new Rectangle();

        public int Speed { get { return _speed; } set { if (value >= 0 && value <= 500) { _speed = value; } } }
        public int Behaviour { get; set; }
        /*behaviour types
         * 0: stationary
         * 1: up and down cycle
         * 2: left and right cycle
         * 3: chases player
         * */

        public List<int[]> RouteCoordinates { get { return _routeCoordinates; } set { _routeCoordinates = value; } }
        public int[] CurrentTarget { get { return _currentTarget; } set { _currentTarget = value; } }

        public int[] Location 
        { get { return _location; }
            set
            {
                if (value.Length == 2)
                {
                    if(value[0] >= 0 && value[0] <= 830 && value[1] >= 0 && value[1] <= 830)
                    {
                        _location = value;
                    }
                }
            }
        }

        public abstract void UpdateLocation();

        public abstract int[] PreviewUpdatedLocation();

        public abstract string GetType();

        

        

    }
}
