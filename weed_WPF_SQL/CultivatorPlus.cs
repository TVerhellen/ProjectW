using System.Collections.Generic;
using System.Windows.Media;

namespace weed_WPF_SQL
{
    public partial class Cultivator
    {
        private List<PointCollection> _positieCultivators;

        public List<PointCollection> PositieCultivators
        {
            get { return _positieCultivators; }
            set { _positieCultivators = value; }
        }

        public override string ToString()
        {
            return $"{NameID}";
        }
    }


}
