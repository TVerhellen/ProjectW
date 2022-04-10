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
    
    public partial class Farm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Farm()
        {
            this.Character = new HashSet<Character>();
            this.Cultivators = new HashSet<Cultivator>();
        }
    
        public int FarmID { get; set; }
        public int CharacterID { get; set; }
        public Nullable<int> LightingID { get; set; }
        public Nullable<int> HeatingID { get; set; }
        public Nullable<int> HumidityID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Character> Character { get; set; }
        public virtual Character Character1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cultivator> Cultivators { get; set; }
        public virtual Heating Heating { get; set; }
        public virtual Humidity Humidity { get; set; }
        public virtual Light Light { get; set; }
    }
}
