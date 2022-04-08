using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace weed_WPF_SQL
{
    internal class Buyer : GameCharacter
    {
        private int _demand;
        private int _money;

        public Buyer(int[] location, int demand, int money)
        {
            Location = location;
            Fill = Brushes.Lime;
            Behaviour = 0;
            Demand = demand;
            Money = money;
            Speed = 0;
            Figure.Width = 20;
            Figure.Height = 20;
            Figure.Fill = Fill;
            Figure.Stroke = Brushes.Black;
        }

        public int Demand { get { return _demand; } set { _demand = value; } }
        public int Money { get { return _money; } set { _money = value; } }

        public override string GetType()
        {
            return "buyer";
        }

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
