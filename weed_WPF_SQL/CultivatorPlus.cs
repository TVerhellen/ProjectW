using System.Collections.Generic;
using System.Windows.Media;

namespace weed_WPF_SQL
{
    public partial class Cultivator
    {
        
        

        public int CyclesPassedPlus
        {
            get { return CyclesPassed; }
            set { CyclesPassed = value; }
        }

        public override string ToString()
        {
            return $"{NameID}";
        }
    }


}
