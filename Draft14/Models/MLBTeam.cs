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
    
    public partial class MLBTeam
    {
        public MLBTeam()
        {
            this.Players = new HashSet<Player>();
        }
    
        public string MLBTeamAbbr { get; set; }
    
        public virtual ICollection<Player> Players { get; set; }
    }
}