using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace weed_WPF_SQL
{
    internal class Cop : GameCharacter
    {
        private int _copType;

        public Cop()
        {
            Fill = Brushes.Blue;
        }

        public int CopType { get { return _copType; } set { _copType = value; } }

        public override int[] PreviewUpdatedLocation()
        {
            throw new NotImplementedException();
        }

        public override void UpdateLocation()
        {
            throw new NotImplementedException();
        }
    }
}
