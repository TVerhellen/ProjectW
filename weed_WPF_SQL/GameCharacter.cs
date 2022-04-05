using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weed_WPF_SQL
{
    internal abstract class GameCharacter
    {
        private int[] _location;
        private int _speed;

        public int Speed { get { return _speed; } set { if (value >= 0 && value <= 5) { _speed = value; } } }
        public int Behaviour { get; set; }

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
    }
}
