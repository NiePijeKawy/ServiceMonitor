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
    
    public partial class PODGiK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PODGiK()
        {
            this.Servicee = new HashSet<Servicee>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdAddress { get; set; }
        public string WebSite { get; set; }
        public string PhoneNumber { get; set; }
    
        public virtual Addresss Addresss { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Servicee> Servicee { get; set; }
    }
}