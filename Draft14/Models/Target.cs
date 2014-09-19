//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Draft14.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Target
    {
        public Target()
        {
            this.RotoTeams = new HashSet<RotoTeam>();
        }
    
        public int TargetId { get; set; }
        public string TargetName { get; set; }
        public int ProjAB { get; set; }
        public int ProjH { get; set; }
        public int ProjHR { get; set; }
        public int ProjRS { get; set; }
        public int ProjRBI { get; set; }
        public int ProjSB { get; set; }
        public int ProjOuts { get; set; }
        public int ProjER { get; set; }
        public int ProjWH { get; set; }
        public int ProjW { get; set; }
        public int ProjSV { get; set; }
        public int ProjK { get; set; }
        public decimal Price { get; set; }
        public decimal PriceBat { get; set; }
        public decimal PricePit { get; set; }
    
        public virtual ICollection<RotoTeam> RotoTeams { get; set; }
    }
}