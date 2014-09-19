using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Draft14.Models;

namespace Draft14.ViewModels
{
    public class MLBRosterViewModel
    {
        public MLBTeam MLBTeam { get; private set; }
        public RotoTeam TargetRotoTeam { get; private set; }
        public IEnumerable<Position> Positions { get; private set; }

        public MLBRosterViewModel(MLBTeam mlbTeam, RotoTeam rotoTeam, IEnumerable<Position> positions)
        {
            MLBTeam = mlbTeam;
            TargetRotoTeam = rotoTeam;
            Positions = positions;
        }

    }
}