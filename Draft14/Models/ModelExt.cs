using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Draft14.Models
{
    public class ModelExt
    {
    }


    public partial class Player
    {
        public string ProjAvg
        {
            get {
                if (this.ProjAB == 0) return ".000";
                else return ((float)ProjH / ProjAB).ToString("F3");
            }
        }

        public string ProjIP
        {
            get
            {
                return (ProjOuts / 3).ToString() + "." + (ProjOuts % 3).ToString();
            }
        }

        public string ProjERA
        {
            get
            {
                if (ProjOuts == 0) return "0.00";
                else return ((float)ProjER * 27.0 / ProjOuts).ToString("F2");
            }
        }

        public string ProjWHIP
        {
            get
            {
                if (ProjOuts == 0) return "0.0000";
                else return ((float)ProjWH * 3.0 / ProjOuts).ToString("F4");
            }
        }
    
    
    }

}