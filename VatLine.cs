using System;
using System.Collections.Generic;
using System.Text;


namespace VatBoardCons
{
    public class VatLine
    {
        public string callsign { get; set; }
        public string planned_depairport { get; set; }
        public string planned_destairport { get; set; }
        public string planned_aircraft { get; set; }
        public string groundspeed { get; set; }
        public string planned_tascruise { get; set; }
        public string altitude { get; set; }
        public double DistanceTo { get; set; }
        public double DistanceFrom { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string MyProperty { get; set; }

    }
}
