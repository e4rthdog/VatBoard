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
using System.Collections.Generic;
using System.Linq;

namespace VatsimData
{
    public class Rating
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("short")]
        public string Short { get; set; }
        [JsonProperty("long")]
        public string Long { get; set; }
        public override string ToString()
        {
            return Short;
        }
        public List<Controller> Controllers
        {
            get
            {
                return VatsimData.Data.Controllers.Where(c => c.Rating == this).ToList();
            }
        }
    }

    public class PilotRating
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("short_name")]
        public string Short { get; set; }
        [JsonProperty("long_name")]
        public string Long { get; set; }
        public override string ToString()
        {
            return Short;
        }
        public List<Pilot> Pilots
        {
            get
            {
                return VatsimData.Data.Pilots.Where(c => c.Rating == this).ToList();
            }
        }
    }
}
