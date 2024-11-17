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

namespace VatsimData
{
    public struct FlightPlan
    {
        [JsonProperty("flight_rules")]
        public string FlightRules { get; set; }
        [JsonProperty("aircraft")]
        public string Aircraft { get; set; }
        [JsonProperty("departure")]
        public string Departure { get; set; }
        [JsonProperty("arrival")]
        public string Arrival { get; set; }
        [JsonProperty("alternate")]
        public string Alternate { get; set; }
        [JsonProperty("cruise_tas")]
        public string CruiseTAS { get; set; }
        [JsonProperty("altitude")]
        public string Altitude { get; set; }
        [JsonProperty("deptime")]
        public string DepartureTime { get; set; }
        [JsonProperty("enroute_time")]
        public string EnrouteTime { get; set; }
        [JsonProperty("fuel_time")]
        public string FuelTime { get; set; }
        [JsonProperty("remarks")]
        public string Remarks { get; set; }
        [JsonProperty("route")]
        public string Route { get; set; }
        public override string ToString()
        {
            return string.Format("{0}-{1} ({2}FR)", Departure, Arrival, FlightRules);
        }
    }
}
