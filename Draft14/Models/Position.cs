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
    
    public partial class Position
    {
        public Position()
        {
            this.Drafteds = new HashSet<Drafted>();
        }
    
        public int PosMask { get; set; }
        public int PosPriority { get; set; }
        public string PosName { get; set; }
        public int RosterSlots { get; set; }
        public int Derived { get; set; }
        public Nullable<int> AuxPosition { get; set; }
    
        public virtual ICollection<Drafted> Drafteds { get; set; }
        public virtual PositionAnalysis PositionAnalysis { get; set; }
    }
}
