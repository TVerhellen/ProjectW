using System;

namespace weed_WPF_SQL
{
    public partial class Cultivator
    {
        private int _titelBar;

        public int Titelbar
        {
            get { return _titelBar; }
            set { _titelBar = value; }
        }

        public int? FertilizerSupplyPlus
        {
            get { return FertilizerSupply; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                    PlantNoFertilizerEvent?.Invoke(this);
                }
                else
                {
                    FertilizerSupply = value;
                }
            }
        }
        public int? WaterSupplyPlus
        {
            get { return WaterSupply; }
            set
            {
                WaterSupply = value;

                if (value < 0)
                {
                    value = 0;
                    PlantNoWaterEvent.Invoke(this);
                }
                else
                {
                    WaterSupply = value;
                }
            }

        }

        public double? RendementValuePlus
        {
            get { return RendementValue; }
            set
            {
                RendementValue = value;
                if (value == 0)
                {
                    NoHarvestEvent?.Invoke(this);
                }
                if (value == 2)
                {
                    PlantAlmostDied?.Invoke(this);
                }
                HealthMonitorEvent?.Invoke(this);
            }
        }

        public int CyclesPassedPlus
        {
            get { return CyclesPassed; }
            set
            {
                CyclesPassed = value;

                if (CyclesPassed == 2 || (CyclesPassed == 5 && WaterSupply > 1))
                {
                    PlantWaterRequirementEvent?.Invoke(this);
                }
                else if (CyclesPassed == 3 && FertilizerSupply > 1)
                {
                    PlantFertilizerRequirementEvent?.Invoke(this);
                }
                else if (CyclesPassed == 9)
                {
                    WaterSupplyPlus = 3;
                    FertilizerSupplyPlus = 5;
                }
                else if (CyclesPassed == 10)
                {
                    NoHarvestEvent?.Invoke(this);
                }

                if (WaterSupplyPlus <= 0 || FertilizerSupplyPlus <= 0)
                {
                    RendementValuePlus--;
                    if (WaterSupplyPlus <= 0 && FertilizerSupplyPlus <= 0)
                    {
                        RendementValuePlus--;
                    }
                }
                else
                {
                    if(CyclesPassedPlus < 9)
                    {
                        RendementValuePlus++;
                    }
                }


                ProgressMonitorEvent?.Invoke(this);
            }
        }


        public override string ToString()
        {
            string resultaat;
            if (Titelbar > 0)
            {
                resultaat = "Plantnr.".PadRight(30) + "Strainnaam".PadRight(30) + Environment.NewLine;
            }
            else if (CyclesPassed < 9)
            {
                resultaat = $"{DataManager.GetStrainNameofCultivator(this)} \n";
            }
            else
            {
                resultaat = "--Selecteer om een plant toe te voegen-- \n";
            }
            return resultaat;
        }

        public delegate void CultivatorHandler(Cultivator obj);

        public event CultivatorHandler PlantWaterRequirementEvent;
        public event CultivatorHandler PlantNoWaterEvent;
        public event CultivatorHandler PlantFertilizerRequirementEvent;
        public event CultivatorHandler PlantNoFertilizerEvent;
        public event CultivatorHandler CyclesEndedEvent;
        public event CultivatorHandler PlantAlmostDied;
        public event CultivatorHandler ProgressMonitorEvent;
        public event CultivatorHandler HealthMonitorEvent;
        public event CultivatorHandler NoHarvestEvent;

    }


}
