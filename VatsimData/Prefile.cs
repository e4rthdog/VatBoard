//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.If not, see<https://www.gnu.org/licenses/>.
//
using Newtonsoft.Json;
using System;

namespace VatsimData
{
    public class Prefile
    {
        [JsonProperty("cid")]
        public string CID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("callsign")]
        public string Callsign { get; set; }
        [JsonProperty("flight_plan")]
        public FlightPlan? FlightPlan { get; set; }
        [JsonProperty("last_updated")]
        private string last_updated;
        public DateTime Last_Updated { get => DateTime.Parse(last_updated); }
        public override string ToString()
        {
            return Callsign;
        }
    }
}
