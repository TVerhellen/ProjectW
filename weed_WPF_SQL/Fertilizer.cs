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
    
    public partial class Fertilizer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fertilizer()
        {
            this.Cultivator = new HashSet<Cultivator>();
        }
    
        public int FertilizerID { get; set; }
        public string Name { get; set; }
        public int AddedNutrition { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cultivator> Cultivator { get; set; }
    }
}
