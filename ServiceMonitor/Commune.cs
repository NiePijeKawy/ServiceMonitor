//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceMonitor
{
    using System;
    using System.Collections.Generic;
    
    public partial class Commune
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Commune()
        {
            this.Addressses = new HashSet<Addresss>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> CountyCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Addresss> Addressses { get; set; }
    }
}
