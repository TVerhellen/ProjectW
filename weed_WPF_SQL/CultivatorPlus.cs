using System;
using System.Windows.Threading;

namespace weed_WPF_SQL
{
    public partial class Cultivator
    {
        private int _waterSupply;
        private int _fertilizerSupply;
        private DateTime _startuur;
        private DispatcherTimer _tim;

        public DispatcherTimer Tim
        {
            get { return _tim; }
            set { _tim = value; }
        }

        public DateTime MyProperty
        {
            get { return _startuur; }
            set { _startuur = value; }
        }
        public int? FertilizerSupplyPlus
        {
            get { return FertilizerSupply; }
            set
            {
                FertilizerSupply = value;
            }
        }
        public int? WaterSupplyPlus
        {
            get { return WaterSupply; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                if (value == 0)
                {
                    PlantNoWaterEvent?.Invoke();
                }

                WaterSupply = value;
            }

        }

        public double? RendementValuePlus
        {
            get { return RendementValue; }
            set
            {
                RendementValue = value;
                if (value <= 4)
                {
                    PlantAlmostDied?.Invoke();
                }
                HealthMonitorEvent?.Invoke();
            }
        }

        public int CyclesPassedPlus
        {
            get { return CyclesPassed; }
            set
            {
                CyclesPassed = value;

                if (CyclesPassed == 3 || CyclesPassed == 7)
                {
                    PlantWaterRequirementEvent?.Invoke();
                }
                else if (CyclesPassed == 5)
                {
                    PlantFertilizerRequirementEvent?.Invoke();
                }

                if (WaterSupply == 0 || FertilizerSupply == 0)
                {
                    RendementValuePlus--;
                    if (WaterSupply == 0 && FertilizerSupply == 0)
                    {
                        RendementValuePlus--;
                    }
                }
                else
                {
                    RendementValuePlus++;
                }
                WaterSupply--;
                FertilizerSupply--;

                ProgressMonitorEvent?.Invoke();
            }
        }


        public override string ToString()
        {
            return $"{NameID}";
        }

        public delegate void CultivatorHandler();

        public event CultivatorHandler PlantWaterRequirementEvent;
        public event CultivatorHandler PlantNoWaterEvent;
        public event CultivatorHandler PlantFertilizerRequirementEvent;
        public event CultivatorHandler PlantNoFertilizerEvent;
        public event CultivatorHandler CyclesEndedEvent;
        public event CultivatorHandler PlantAlmostDied;
        public event CultivatorHandler ProgressMonitorEvent;
        public event CultivatorHandler HealthMonitorEvent;

    }


}
