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
    
    public partial class ChangeStageWork
    {
        public int Id { get; set; }
        public System.DateTime SinceWhen { get; set; }
        public Nullable<System.DateTime> UntilWhen { get; set; }
        public int IdWorkStage { get; set; }
        public int IdService { get; set; }
        public string Descriptionn { get; set; }
    
        public virtual Servicee Servicee { get; set; }
        public virtual WorkStage WorkStage { get; set; }
    }
}
