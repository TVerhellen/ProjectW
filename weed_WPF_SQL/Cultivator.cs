//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace weed_WPF_SQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cultivator
    {
        public int CultivatorID { get; set; }
        public int FarmID { get; set; }
        public int CultID { get; set; }
        public int WaterID { get; set; }
        public int FertilizerID { get; set; }
        public int SoilID { get; set; }
        public int LampID { get; set; }
        public int CyclesRequired { get; set; }
        public int CyclesPassed { get; set; }
        public Nullable<int> NameID { get; set; }
        public Nullable<double> RendementValue { get; set; }
        public Nullable<int> ProgresBarColor { get; set; }
        public Nullable<int> WaterSupply { get; set; }
        public Nullable<int> FertilizerSupply { get; set; }
    
        public virtual CultType CultType { get; set; }
        public virtual Farm Farm { get; set; }
        public virtual Fertilizer Fertilizer { get; set; }
        public virtual Lamp Lamp { get; set; }
        public virtual Name Name { get; set; }
        public virtual Soil Soil { get; set; }
        public virtual WaterSystem WaterSystem { get; set; }
    }
}
