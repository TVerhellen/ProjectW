using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weed_WPF_SQL
{
    public partial class Character
    {
        public override string ToString()
        {
            string display = this.Name.ToString().PadLeft(20) + this.Money.ToString().PadLeft(10) + " - " + this.Reputation.ToString();

            return display;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
